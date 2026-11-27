using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using WindowsFormsApp1;


namespace WindowsForm_DianPIan
{
    /// <summary>
    /// 检测主页（主窗体）
    /// 职责：① 初始化 GigE 相机与采集 FIFO；② 接收触发后的图像并显示；
    ///       ③ 转发相机参数给参数子窗体；④ 用本地图片跑 VPP 工具块验证检测逻辑；
    ///       ⑤ 累计 OK/NG/总数与良品率，把每次检测写进检测记录表并追加到 CSV。
    /// 说明：窗体本身不参与图像处理算法，算法全部封装在 VPP 工具块里，本窗体只做"驱动 + 显示 + 记录"。
    /// </summary>
    public partial class MainForm : Form
    {
        #region 一、检测类型枚举
        /// <summary>
        /// 检测类型：给检测记录和 CSV 标记本次检测属于哪一类工艺
        /// 注意：ToString() 会直接把枚举名写成中文进文件，改名等于改文件内容，非必要不要动
        /// </summary>
        public enum CheckType
        {
            缺陷检测,   // 外观缺陷类（胶点、划伤、脏污等）
            测量,       // 尺寸测量类（半径、圆心、角度等）
            识别,       // 字符 / 条码识别类
            引导        // 定位引导类（给出抓取坐标）
        }
        #endregion

        #region 二、全局成员变量
        /// <summary>
        /// 相机抓取器：代表单台GigE相机设备
        /// 提供相机级操作（取视频格式、创建FIFO、读写相机Feature）
        /// </summary>
        private ICogFrameGrabber frameGrabber;

        /// <summary>
        /// 采集FIFO队列：VisionPro相机采集核心对象，负责缓存图像、管理触发、采集队列
        /// 一次StartAcquire对应FIFO里一个采集请求，取图靠CompleteAcquireEx
        /// </summary>
        private ICogAcqFifo acqFifo;

        /// <summary>
        /// 从FIFO取出的Cog图像对象
        /// 注意：它在Complete回调（子线程）里被赋值，不要在该线程外当作线程安全变量随意读写
        /// </summary>
        private ICogImage cogImage;

        /// <summary>
        /// 窗体关闭标记，用于安全释放资源
        /// 用法约定：FormClosing 里先置 true，Complete 回调入口先判它再决定是否继续采集。
        /// 【现状】全项目搜索只有这一行声明，没有任何读写，编译器会报 CS0169（字段从未使用）；
        ///         要启用就在 FormClosing 里写 isClosing = true，再在 Fifo_Complete 开头判断；
        ///         暂时不用则保持注释，警告即消失。
        /// </summary>
        //private bool isClosing;

        /// <summary>
        /// 相机INI配置文件完整路径：程序运行目录/INI/camera.ini
        /// 与 FrmCameraParams 共用同一个文件，所以节名与键名必须两边一致，改一处要同步另一处
        /// </summary>
        private string iniFile = Path.Combine(Application.StartupPath, "INI", "camera.ini");
        private string iniFile2 = Path.Combine(Application.StartupPath, "INI", "Result.ini");

        /// <summary>
        /// 相机触发模式：true=硬触发，false=软触发
        /// 由INI在窗体加载时决定，决定调用TriggerHandLer还是SoftHandLer
        /// </summary>
        private bool YingChuFa = true;


        // 实例化VisionPro图像文件读取工具：专门用来读取本地图像文件
        //CogImageFileTool cogImageFileTool = new CogImageFileTool();     //bt1 方式一使用
        // ICogImage：VisionPro图像接口对象，用来存放读取成功的图像
        //public ICogImage cogImage;

        /// <summary>
        /// 工具块对象（备用字段）：目前没有实际赋值，旧版从本地图片跑工具时用过，保留不删以免影响其它引用
        /// </summary>
        public CogToolBlock cogToolBlock;

        /// <summary>
        /// 由本地图片转换来的8位灰度Cog图像（方式二）
        /// 用途：既显示到左侧"实时采集图像"，又作为VPP工具块的输入，避免重复转换
        /// </summary>
        private CogImage8Grey cogImage8Grey;

        /// <summary>
        /// 全局单例的工具块容器（SingleToolBlock.Instance）
        /// 由构造函数从单例实例里取出，后续要跑工具时统一走它，避免每个窗体各加载一份VPP
        /// </summary>
        SingleToolBlock singleToolBlock;

        /// <summary>
        /// OK数量
        /// </summary>
        private int OKNum = 0;

        /// <summary>
        /// NG数量
        /// </summary>
        private int NGNum = 0;

        /// <summary>
        /// 总数量
        /// </summary>
        private int TotalNum = 0;

        /// <summary>
        /// 检测记录数据源：一条记录一个 CsvData，绑到 dgvJianCeJiLu 上显示
        /// 注意：表格一旦绑定了数据源，行集合就由它掌管，
        ///       此时不能再用 dgvJianCeJiLu.Rows.Insert / Add 加行，否则抛 InvalidOperationException；
        ///       新增记录要改成 list.Insert(0, new CsvData(...))，由 BindingList 通知表格刷新。
        /// </summary>
        private BindingList<CsvData> list;
        #endregion

        #region 三、构造函数
        /// <summary>
        /// 主窗体构造函数
        /// 做三件事：由设计器生成控件（InitializeComponent）、挂上窗体加载事件、取工具块单例；
        /// 相机的枚举、FIFO 创建等耗时操作一律放到 Load 里，避免构造阶段就去碰硬件。
        /// </summary>
        public MainForm()
        {
            // 创建并布局设计器里的全部控件
            InitializeComponent();
            // 订阅窗体加载事件，相机初始化流程从 MainForm_Load 开始
            this.Load += MainForm_Load;
            // 取出全局单例工具块，保证整个程序只有一份工具配置
            singleToolBlock = SingleToolBlock.Instance;
            // 启动时把本月 CSV 里的历史检测记录读进来，作为表格的数据源，界面一打开就能看到旧记录
            list = CsvAPI.ReadCsv();
            // 把整个列表绑给表格：列表增删元素会自动刷新界面，不用手工 Rows.Add
            // 【注意】绑定的同时设计器里已有关键列，若不设 AutoGenerateColumns=false 会多出 Time/CsvCheckType/... 四列
            this.dgvJianCeJiLu.DataSource = list;


            string OKValue = IniAPI.INIGetStringValue(iniFile2, "检测结果存储", "OK", "0");
            string NGValue = IniAPI.INIGetStringValue(iniFile2, "检测结果存储", "NG", "0");
            string RateValue = IniAPI.INIGetStringValue(iniFile2, "检测结果存储", "Rate", "0");
            string TotalValue = IniAPI.INIGetStringValue(iniFile2, "检测结果存储", "Total", "0");
            OKNum = Convert.ToInt32(OKValue);
            NGNum = Convert.ToInt32(NGValue);
            TotalNum = Convert.ToInt32(TotalValue);
            txtOK.Text = OKValue;
            txtNG.Text = NGValue;
            txtLiangPinLv.Text = ((double)OKNum / TotalNum).ToString("0.00%");
            txtZongShu.Text = TotalValue;
        }
        #endregion

        #region 四、窗体加载 & 相机初始化流程
        /// <summary>
        /// 窗体加载事件
        /// 执行流程：读INI决定触发方式 → 枚举相机 → 取第一个相机 → 创建采集FIFO → 设置触发模式 → 订阅采集完成回调
        /// 注意：任一步失败都要立即return，后面步骤依赖前面对象不为null
        /// 补充：相机只在这里枚举一次，没有重试机制；如果 Count=0 而相机确实在线，
        ///       按"相机未上电、网线/网段不符、Cognex GigE 驱动未就绪"这三条顺序排查。
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // 从INI配置文件中获取"相机触发模式"的值
            // 参数说明:
            // "iniFile" - INI配置文件的路径
            // "相机触发模式" - 配置文件中的节(Section)名称
            // "Hardware" - 配置文件中的键(Key)名称
            // "true" - 默认值：该键不存在时返回"true"，即默认走硬触发
            string value = IniAPI.INIGetStringValue(iniFile, "相机触发模式", "Hardware", "true");

            // 将获取到的字符串值转换为布尔类型并赋值给YingChuFa变量
            // Convert.ToBoolean 遇到非法字符串会直接抛异常，INI被手工改坏时要留意
            YingChuFa = Convert.ToBoolean(value);

            // 1. 实例化相机集合，枚举本机所有可用FrameGrabber相机
            CogFrameGrabbers frame = new CogFrameGrabbers();
            // 判断无相机设备：没有相机后续全部无意义，直接结束加载
            if (frame == null || frame.Count < 1)
            {
                AppendLog("请检测相机是否连接，初始化失败");
                return;
            }
            AppendLog("成功抓取相机");

            // 2. 获取列表中第0台相机
            frameGrabber = frame[0];

            // 3. 创建采集FIFO
            // 参数1：视频格式 GigE Bayer彩色格式
            // 参数2：像素格式 8bit灰度
            // 参数3：相机端口 0
            // 参数4：autoPrepare=true，自动初始化硬件
            acqFifo = frameGrabber.CreateAcqFifo("Generic GigEVision (Bayer Color)", CogAcqFifoPixelFormatConstants.Format8Grey, 0, true);

            if (YingChuFa)
            {
                TriggerHandLer();           // 硬触发模式：外部IO上升/下降沿触发拍照
            }
            else
            {
                SoftHandLer();              // 软触发模式：软件发送触发命令拍照
            }

            // 4. 订阅FIFO采集完成事件：相机图像就绪后触发回调（子线程）
            acqFifo.Complete += Fifo_Complete;
            serialPort1.Open();
            serialPort1.DataReceived += Sp_DataReceived;

        }

        /// <summary>
        /// 【设计器生成的空 Load 事件，名字带 _1】
        /// 来源：在 VS 设计器里双击窗体误生成，MainForm.Designer.cs 里有一行 this.Load += MainForm_Load_1；
        /// 注意：构造函数里又手工订阅了 MainForm_Load，等于 Load 被绑了两次——
        ///       当前方法体是空的所以看不出问题，一旦往里写初始化代码就会执行两遍。
        /// 建议：确认无用后，删掉 Designer.cs 里那行订阅，再删掉本方法，只保留 MainForm_Load。
        /// </summary>
        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
        #endregion

        #region 五、触发模式配置方法
        /// <summary>
        /// 硬触发配置方法
        /// 触发源：相机物理Line输入信号（外部传感器、PLC信号）
        /// 两层配置要分清：
        ///   ① VisionPro侧 OwnedTriggerParams —— 只决定"FIFO怎么取图"，不会把 TriggerMode 写进相机；
        ///   ② 相机侧 OwnedGigEAccess.SetFeature —— 才真正改相机内部的 TriggerSource / TriggerActivation。
        /// 【关键】相机侧还有一个 TriggerMode 必须为 On 才真正等边沿，本方法没有写它：
        ///   相机上电默认 TriggerMode=Off，此时相机自由连续出图，StartAcquire 立刻拿到一帧，
        ///   表现就是"程序一打开就有画面"。要真正只靠硬触发取图，需补
        ///   SetFeature("TriggerMode","On")，并用 GetFeature("TriggerMode") 读回校验，
        ///   配置完成后调用 acqFifo.Flush() 丢掉使能瞬间悬空电平引起的那一帧。
        /// 调用时机：FIFO创建完成后，且相机确实接了触发线，否则StartAcquire会一直等不到边沿。
        /// </summary>
        private void TriggerHandLer()
        {
            // 【VisionPro侧】设置触发模型为手动：StartAcquire排一个请求，等触发信号到了才完成取图
            acqFifo.OwnedTriggerParams.TriggerModel = CogAcqTriggerModelConstants.Manual;
            // 【VisionPro侧】打开触发参数（只影响VisionPro的取图行为，不等于打开了相机的TriggerMode）
            acqFifo.OwnedTriggerParams.TriggerEnabled = true;
            // 【相机侧】设置触发源为硬触发line0（相机具体有哪几根线由相机能力决定，用GetValidEnumValues可查）
            acqFifo.FrameGrabber.OwnedGigEAccess.SetFeature("TriggerSource", "Line0");
            // 【相机侧】设置触发极性：上升沿触发
            acqFifo.FrameGrabber.OwnedGigEAccess.SetFeature("TriggerActivation", "RisingEdge");
        }

        /// <summary>
        /// 软触发配置方法
        /// 触发源：软件命令触发，调用StartAcquire之后由软件发一次触发信号才曝光
        /// 与硬触发的区别：只把相机侧 TriggerSource 改成 Software，VisionPro侧模型同样是 Manual；
        /// 注意：这里同样没有写相机侧 TriggerMode=On，若相机处于 Off 状态仍会自由出图。
        /// </summary>
        private void SoftHandLer()
        {
            // 【VisionPro侧】设置触发模型为手动，StartAcquire 之后不会自动取到图
            acqFifo.OwnedTriggerParams.TriggerModel = CogAcqTriggerModelConstants.Manual;
            // 【VisionPro侧】打开触发参数
            acqFifo.OwnedTriggerParams.TriggerEnabled = true;
            // 【相机侧】设置触发源为软触发（软件触发信号由相机驱动发送）
            acqFifo.FrameGrabber.OwnedGigEAccess.SetFeature("TriggerSource", "Software");
        }
        #endregion

        #region 六、FIFO采集完成回调事件
        /// <summary>
        /// FIFO图像就绪回调
        /// 执行线程：子线程！所有UI操作必须保证线程安全
        /// 触发时机：相机曝光完成、图像进入FIFO就绪队列
        /// 补充：只要相机侧 TriggerMode=Off（自由运行），这个回调会连续不断地被触发，
        ///       画面看起来就是"实时视频"；想做到"一次触发一张图"，前提是相机侧触发真的被打开。
        /// 注意：crdYuanTu 是ActiveX控件，必须回到UI线程才能赋值（详见下方标记处）
        /// 现状：本回调只做"查状态 → 取图 → 贴图"，没有运行工具块，
        ///       所以相机这条链路目前只出画面、不出 OK/NG，也不会写检测记录。
        /// </summary>
        private void Fifo_Complete(object sender, CogCompleteEventArgs e)
        {
            try
            {
                // 查询FIFO状态
                // numPending：等待采集任务数
                // numReady：已经采集完成、待取出图像数量
                // busy：相机是否正在采集
                acqFifo.GetFifoState(out int numPending, out int numReady, out bool busy);

                // 队列存在就绪图像，取出图像
                if (numReady > 0)
                {
                    CogAcqInfo cogAcqInfo = new CogAcqInfo();
                    // 从FIFO取出一张图像，同时带出采集附加信息
                    // 若本次采集异常终止，这里会抛 CogAcqAbnormalException，交给catch记日志
                    cogImage = acqFifo.CompleteAcquireEx(cogAcqInfo);

                    // 图像绑定显示控件
                    // 【风险点】这两句运行在采集子线程，直接操作ActiveX控件会引发COM跨套间调用，
                    //           轻则抛异常、重则纯虚函数崩溃(R6025)，应改为BeginInvoke回UI线程后再赋值
                    this.crdYuanTu.Image = cogImage;

                    // 图像自适应显示窗口大小
                    this.crdYuanTu.Fit();
                }
            }
            catch (Exception ex)
            {
                // 采集异常不向外抛：Complete事件里抛出去会直接打断程序
                AppendLog(ex.ToString());
            }
        }
        #endregion

        #region 七、跨线程UI更新工具（日志 / 检测记录）

        #region 日志输出
        /// <summary>
        /// 日志输出方法
        /// 特性：自动跨线程安全处理，向lstRiZhi输出带时间戳日志
        /// 说明：FIFO Complete 回调运行在采集子线程，直接操作控件会抛"跨线程操作无效"，
        ///       所以这里统一用 InvokeRequired 判断 + Invoke 同步调度。
        /// 为什么用 Invoke 而不是 BeginInvoke：日志属于"必须留下"的信息，同步调度能保证
        ///       调用方返回时日志已经进了列表；日志量小，不会拖慢采集节奏。
        /// </summary>
        /// <param name="msg">日志文本</param>
        private void AppendLog(string msg)
        {
            // InvokeRequired=true：当前代码在非UI子线程，需要调度回主线程
            if (this.lstRiZhi.InvokeRequired)
            {
                // 委托调度到UI线程，递归执行日志写入（递归进来时 InvokeRequired 已为 false，走 else）
                this.lstRiZhi.Invoke(new Action(() => AppendLog(msg)));
            }
            else
            {
                // 日志插入头部，最新日志在最上方
                this.lstRiZhi.Items.Insert(0, $"{DateTime.Now.ToString()}: {msg}");
                // 自动选中第一条，滚动定位最新日志
                this.lstRiZhi.SelectedIndex = 0;
            }
        }
        #endregion

        #region 检测记录写入
        /// <summary>
        /// 向检测记录表 dgvJianCeJiLu 追加一行数据
        /// 列顺序（设计器现状）：时间 / 检测类型 / 结果 / 备注，四个值必须与这四列一一对应
        /// 线程处理：本方法可能被采集子线程调用，用 InvokeRequired 判断 + BeginInvoke 异步转交；
        ///           BeginInvoke 投递完立即返回，不会把采集线程卡在 UI 上，
        ///           而 Invoke 要等 UI 执行完才返回，用在逐帧回调里会拖慢采图。
        /// 行数控制：只保留最近 500 行，长时间连续检测不加限制会让表格越来越卡。
        /// 【注意】构造函数里已经给表格绑定了 DataSource，绑定状态下不能再改 Rows 集合；
        ///         下面这行 Rows.Insert 会在运行时抛 InvalidOperationException（控件绑定数据时不能以编程方式添加行），
        ///         正确做法是改成 list.Insert(0, new CsvData(...))，由 BindingList 通知表格刷新。
        /// </summary>
        /// <param name="shuJu">检测类型列文本（注意：现在传进来的是"合格数量:5/5"，与列语义不符）</param>
        /// <param name="jieGuo">结果列文本，OK 或 NG，用于着色</param>
        private void AppendJianCeJiLu(string shuJu, string jieGuo)
        {
            // InvokeRequired=true：当前在非UI子线程，需要把这次调用转交给主线程执行
            if (dgvJianCeJiLu.InvokeRequired)
            {
                // BeginInvoke(委托, 参数1, 参数2)：把"要调用的方法 + 参数"打包成一条消息，投递到主线程的消息队列
                // 这里的 return 不能省：否则子线程会继续往下走，既跨线程碰控件，
                // 又会在主线程那次调用之外重复插一行记录
                dgvJianCeJiLu.BeginInvoke(new Action<string, string>(AppendJianCeJiLu), shuJu, jieGuo);
                return;
            }

            // 插到第 0 行：时间、检测类型、结果、备注，顺序与设计器里四列的左右顺序一致
            // 【待改】表格已绑定 DataSource，这一行在运行时会抛 InvalidOperationException，
            //         应替换为：list.Insert(0, new CsvData(时间, CheckType.缺陷检测, jieGuo, 备注));
            dgvJianCeJiLu.Rows.Insert(0, DateTime.Now.ToString("HH:mm:ss"), shuJu, jieGuo, "轴承检测");

            // 取刚插入的那一行（索引 0 就是它），给结果列的单元格着色
            DataGridViewRow row = dgvJianCeJiLu.Rows[0];
            // Cells["colJieGuo"] 用的是列的 Name 属性，不是表头文字"结果"；写错会抛 ArgumentException
            // 三元运算符：结果为 OK 取森林绿，否则（NG）取深红
            row.Cells["colJieGuo"].Style.ForeColor = jieGuo == "OK" ? Color.ForestGreen : Color.Crimson;

            // 只保留最近 500 条：超出的从最末尾（也就是最旧的那行）逐行删掉
            const int maxRows = 500;
            while (dgvJianCeJiLu.Rows.Count > maxRows)
            {
                dgvJianCeJiLu.Rows.RemoveAt(dgvJianCeJiLu.Rows.Count - 1);
            }
        }
        #endregion

        #endregion

        #region 八、界面控件事件（绘制 / 按钮）
        /// <summary>
        /// 左侧容器（tlpZuoCe）的重绘事件
        /// 当前是空实现，只保留挂点：
        /// Paint 在窗体显示、拖拽、被其它窗口遮挡后恢复时都会被反复触发，
        /// 所以不要在这里写"只应执行一次"的逻辑（例如StartAcquire），否则会重复排采集任务。
        /// </summary>
        private void tlpZuoCe_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 【执行】按钮点击事件
        /// 作用：向FIFO排一个采集请求，只负责"下单"，真正的取图在 Fifo_Complete 回调里完成；
        /// 前提：相机已初始化（acqFifo 不为 null），否则只记一条日志直接返回。
        /// </summary>
        private void btnZhiXing_Click(object sender, EventArgs e)
        {
            if (acqFifo == null)
            {
                AppendLog("相机没有连接");
                return;
            }
            // 启动FIFO采集任务
            acqFifo.StartAcquire();
        }
        #endregion

        #region 九、本地图像读取（【选择图像】按钮 + 工具运行）
        /// <summary>
        /// 【选择图像】按钮点击事件：不经过相机，直接把本地图片读成Cog图像，
        /// 再喂给VPP工具块运行，结果显示在右侧"处理结果图像"里。
        /// 用途：没有相机、或相机没接好时也能验证工具块配置是否正确。
        /// </summary>
        private void btnXuanZeTuXiang_Click(object sender, EventArgs e)
        {
            #region 方式一：使用CogImageFileTool读取本地图片
            //// 创建文件打开对话框实例，供用户在电脑上选择图片
            //OpenFileDialog openFileDialog = new OpenFileDialog();

            //// 设置文件过滤规则：下拉选项1=图像文件，支持bmp/jpg/jpeg/png/tif/tiff；选项2=所有文件
            //openFileDialog.Filter = "图像文件|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|所有文件|*.*";
            //// 设置打开对话框窗口标题
            //openFileDialog.Title = "选择图像文件";
            //// RestoreDirectory=true：关闭对话框后，自动恢复到上次打开的文件夹路径
            //openFileDialog.RestoreDirectory = true;

            //// 弹出选择文件窗口；判断用户点击【打开】（DialogResult.OK）
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    // 获取用户选中文件的完整绝对路径
            //    string filePath = openFileDialog.FileName;    

            //    // 调用图像工具打开文件，打开模式：只读
            //    cogImageFileTool.Operator.Open(filePath, CogImageFileModeConstants.Read);
            //    // 运行图像读取工具，把图片加载进工具内部
            //    cogImageFileTool.Run();

            //    // 将读取出来的输出图像赋值给全局变量cogImage保存
            //    cogImage = cogImageFileTool.OutputImage;
            //    // 把图像赋值给界面上第一个VisionPro显示控件 crdYuanTu
            //    crdYuanTu.Image = cogImageFileTool.OutputImage;
            //    // Fit()：图像自动缩放，适配显示控件窗口大小
            //    crdYuanTu.Fit();
            //}
            #endregion

            #region 方式二 ：使用C#自带的Bitmap读取本地图片，然后转换为VisionPro图像对象
            // 创建文件打开对话框实例，供用户在电脑上选择图片
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置文件过滤规则：下拉选项1=图像文件，支持bmp/jpg/jpeg/png/tif/tiff；选项2=所有文件
            openFileDialog.Filter = "图像文件|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff|所有文件|*.*";
            // 设置打开对话框窗口标题
            openFileDialog.Title = "选择图像文件";
            // RestoreDirectory=true：关闭对话框后，自动恢复到上次打开的文件夹路径
            openFileDialog.RestoreDirectory = true;

            // 弹出选择文件窗口；判断用户点击【打开】（DialogResult.OK）
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // ===== 第1步：拿到用户选中图片的绝对路径并读成 Bitmap =====
                    string filePath = openFileDialog.FileName;
                    Bitmap bitmap = Bitmap.FromFile(filePath) as Bitmap;

                    // ===== 第2步：Bitmap 转成 VisionPro 的 CogImage8Grey，并显示到左侧"实时采集图像" =====
                    // VisionPro 工具只认 CogImage 家族，GDI+ 的 Bitmap 不能直接喂给工具，必须先转换
                    cogImage8Grey = new CogImage8Grey(bitmap);
                    // 把图像赋值给界面上的实时采集图像控件 crdYuanTu
                    crdYuanTu.Image = cogImage8Grey;
                    // Fit()：图像自动缩放，适配显示控件窗口大小
                    crdYuanTu.Fit();

                    // ===== 第3步：从全局单例取出工具块，把待检测图像填进第一个输入端子 =====
                    // 用单例而不是每次 LoadObjectFromFile，既省掉重复反序列化的时间，也保证整个程序只有一份工具配置
                    CogToolBlock toolBlock = singleToolBlock.ToolBlock;
                    // Inputs[0] 是工具块的第一个输入端子，约定用来接收待检测图像
                    toolBlock.Inputs[0].Value = cogImage8Grey;
                    // 运行整条工具块：取像→找边→测量→判定，按 VPP 里保存的顺序执行
                    toolBlock.Run();

                    // ===== 第4步：把运行结果图形显示到右侧"处理结果图像" =====
                    // CreateLastRunRecord() 生成本次运行的图形记录（ROI、边线、标注等），
                    // SubRecords[0] 取最外层记录；直接赋 Record 比赋 Image 更能看到测量标注
                    this.crdChuLiTu.Record = toolBlock.CreateLastRunRecord().SubRecords[0];
                    crdChuLiTu.Fit();

                    // ===== 第5步：取工具块输出端子，得到本张图的 OK 数 / NG 数 / 总数 =====
                    // 三个端子名必须与 VPP 里工具块的实际端子名逐字一致：
                    // 名字写错不会编译报错，而是运行时抛 IndexOutOfRangeException（Collection does not contain an index for XXX）
                    // 用 TryParse 而不是 Convert.ToInt32：端子值为 null 或非数字时不抛异常，只是得到 0
                    int.TryParse(toolBlock.Outputs["OKOutput"].Value.ToString(), out int OKCount);
                    int.TryParse(toolBlock.Outputs["NGOutput1"].Value.ToString(), out int NGCount);
                    int.TryParse(toolBlock.Outputs["TotalCount"].Value.ToString(), out int TotalCount);
                    // 本张图的良品率（保留两位小数），当前版本未参与界面显示，留作备用；
                    // 保留它会得到"未使用变量"的编译警告，不影响运行
                    double rate = Math.Round((double)OKCount / TotalCount, 2);

                    // ===== 第6步：累计统计，刷新右侧"检测结果"的四个数值框 =====
                    // 累加用的是全局总量，代表"程序启动以来"的合计，不是本张图的数量
                    OKNum += OKCount;
                    NGNum += NGCount;
                    TotalNum += TotalCount;
                    txtOK.Text = OKNum.ToString();
                    txtNG.Text = NGNum.ToString();
                    txtZongShu.Text = TotalNum.ToString();
                    // "P" 格式符会自动先乘 100 再拼百分号，P2 表示保留两位小数，例如 100.00%
                    // 前提是 TotalNum 不为 0，否则抛 DivideByZeroException（会被本方法外层 catch 记成"检测失败"）
                    txtLiangPinLv.Text = ((double)OKNum / TotalNum).ToString("P2");

                    // ===== 第7步：写检测记录（界面表格 + CSV 文件，两处都记） =====
                    // 数据列：本次这张图里 合格/总数
                    string shuJu = $"合格数量:{OKCount}/{TotalCount}";
                    // 结果列：本张图里全部合格才算 OK，否则 NG
                    // TotalCount > 0 的判断是为了避免 0/0 被误判成合格
                    string jieGuo = (TotalCount > 0 && OKCount == TotalCount) ? "OK" : "NG";
                    // ① 写界面表格：时间列自动取当前时间，最新一行插在最上面，结果列为 OK 的着绿色
                    //AppendJianCeJiLu(shuJu, jieGuo);
                    AppendLog("检测成功");
                    // ② 追加 CSV：参数依次为 检测类型、检测结果、备注
                    // 文件位置：程序目录\Data\年月.csv（例如 bin\Debug\Data\202611.csv），文件名里的年月由 SaveCsv 内部取当前时间生成
                    // 注意：该文件正被 Excel / WPS 打开时会抛 IOException（共享冲突），属于外部程序占用
                    CsvAPI.SaveCsv(CheckType.缺陷检测, jieGuo, "轴承检测");
                    CsvData csvData = new CsvData(DateTime.Now.ToString("dd HH:mm:ss"), CheckType.缺陷检测, jieGuo, "轴承检测");
                    list.Add(csvData);
                    SaveCheckResult(OKNum, NGNum, (double)OKCount / TotalCount, TotalNum);


                    //SerialPort comResult = ComPortAPI.Send("COM1", jieGuo);
                    //comResult.DataReceived += Sp_DataReceived;
                    //if (comResult)
                    //{
                    //    AppendLog("数据发送成功");
                    //}
                    //else
                    //{
                    //    AppendLog("数据传输失败");
                    //}
                    //serialportl.Open();
                    // 把本次结果（OK / NG）通过串口发给下位机（PLC、报警灯、分拣机构等）
                    // 【隐患】serialPort1 是设计器拖进来的组件，代码里从未调用 Open()，
                    //         端口未打开时 Write 会抛 InvalidOperationException，表现就是
                    //         "CSV 已经写进去了、界面却提示检测失败"。使用前必须先 Open，
                    //         推荐在窗体 Load 里 Open 一次、FormClosing 里 Close。
                    serialPort1.Write(jieGuo);
                    // 【隐患】每检测一次就订阅一次，事件会不断累加：检测 10 次后收到一次数据会触发 10 次回调。
                    //         正确做法是把订阅挪到窗体加载时只做一次，或停止通信时用 -= 取消。
                }
                catch (Exception ex)
                {
                    AppendLog("检测失败: " + ex.Message);
                }
            }

            #endregion
        }
        #endregion

        #region 十、检测记录加载（【加载数据】按钮）
        /// <summary>
        /// 【加载数据】按钮点击事件：重新读一遍本月 CSV，把历史记录刷进表格
        /// 用途：程序运行中新写了记录、或用文本编辑器改过 CSV 之后，点一下就能让界面与文件对齐
        /// 返回 null 表示本月还没有 CSV 文件，此时只记日志、保留界面原有内容，不清空表格
        /// </summary>
        private void btnJiaZaiShuJu_Click(object sender, EventArgs e)
        {
            // 重新全量读取本月 CSV，得到一个新的 BindingList
            BindingList<CsvData> xinLieBiao = CsvAPI.ReadCsv();
            // 文件不存在（本月还没有任何记录）：提示一句就返回，不要把已有表格清空
            if (xinLieBiao == null)
            {
                AppendLog("没有检测到数据，请检查文件是否存在");
                return;
            }
            // 关键：字段 list 与表格 DataSource 必须指向同一个对象，
            // 否则后续新增记录插进旧列表里，界面不会显示
            list = xinLieBiao;
            // 重新绑定数据源，触发整表刷新
            dgvJianCeJiLu.DataSource = list;
        }
        #endregion

        #region 十一、检测统计持久化（INI 读写）
        /// <summary>
        /// 把累计统计写回 Result.ini 的【检测结果存储】节
        /// 用途：让 OK 数、NG 数、总数、良率在程序重启后还能接着累计，而不是每次从 0 开始。
        /// 调用时机：每检测完一张图，在 btnXuanZeTuXiang_Click 里累加完内存计数后调用一次。
        /// 注意：① 写进去的是"程序启动以来累计后的总量"，不是单张图的数量；
        ///       ② IniAPI.INIWriteValue 对节点名、键名为空会抛 ArgumentException，这里三个值都是常量，正常不会触发；
        ///       ③ 每张图连写 4 次磁盘，节拍很快时应改成"退出时统一写一次"或做防抖，否则会拖慢检测。
        /// </summary>
        /// <param name="OK">累计合格数</param>
        /// <param name="NG">累计不合格数</param>
        /// <param name="rate">本次良品率（0~1 的小数，按字符串原样存储，读回时再格式化）</param>
        /// <param name="Total">累计总数</param>
        private void SaveCheckResult(int OK, int NG, double rate, int Total)
        {
            // 四个键名与构造函数里读取的键名必须逐字一致，改一处就要同步另一处
            IniAPI.INIWriteValue(iniFile2, "检测结果存储", "OK", OK.ToString());
            IniAPI.INIWriteValue(iniFile2, "检测结果存储", "NG", NG.ToString());
            IniAPI.INIWriteValue(iniFile2, "检测结果存储", "Rate", rate.ToString());
            IniAPI.INIWriteValue(iniFile2, "检测结果存储", "Total", Total.ToString());
        }
        #endregion

        #region 十二、串口通信
        /// <summary>
        /// 串口数据接收回调（由 SerialPort.DataReceived 触发）
        /// 执行线程：串口自己的后台线程，不是 UI 线程！
        /// 流程：取缓冲区字节数 → 一次性全部读出 → 按 UTF-8 解成字符串
        /// 注意：① 读出来的 data 没有被使用，也没有写日志或显示到界面，属于"收完就丢"，
        ///          要做双向通信应在这里解析协议并 Invoke 回主线程更新界面；
        ///       ② 结尾 sp.Close() 会直接关闭串口——收到一次数据后端口就关了，
        ///          之后再 Write 会抛 InvalidOperationException（端口未打开）；
        ///       ③ 回调在非 UI 线程，碰任何控件都要 Invoke（可参考 AppendLog 的写法）。
        /// </summary>
        public void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // sender 就是触发事件的 SerialPort 实例，转成具体类型后才能读数据
            SerialPort sp = sender as SerialPort;
            // BytesToRead：接收缓冲区里已到达的字节数；为 0 时下面的 new byte[0] 不会报错
            int size = sp.BytesToRead;
            byte[] buffer = new byte[size];
            // 把缓冲区里的字节全部读进 buffer（第三个参数是要读的字节数）
            sp?.Read(buffer, 0, size);
            // 按 UTF-8 解码；若对端用 GBK 发送，中文会变乱码，需换成 Encoding.GetEncoding("GBK")
            string data = Encoding.UTF8.GetString(buffer);
            // 关闭串口：这一步之后端口不可再读写，需要重新 Open 才能继续通信
            sp.Close();
        }
        #endregion

        #region 十三、网口通信（TCP 客户端）
        /// <summary>
        /// 菜单【网口通信】点击事件：把当前累计总数通过 TCP 发给服务端，并把回传内容写进日志
        /// 为什么是 async void：本方法由设计器用 EventHandler 订阅（形参 object + EventArgs、返回 void），
        ///   签名必须完全一致才能挂上 Click 事件；写成 async Task 会编译失败
        ///   （CS0407：返回类型错误）。async void 是事件处理器的标准写法。
        /// 注意：① async void 的异常无法被调用方捕获，会直接抛到 UI 线程弹"未处理异常"，
        ///          这里没有 try/catch，服务端没开时会直接崩，建议包一层；
        ///       ② 发送的是 TotalNum（累计总数），与串口发的是"本次 OK/NG"不是一回事；
        ///       ③ IP 与端口硬编码 172.16.50.15:10000，换现场要改这里或提到 INI 配置。
        /// </summary>
        private async void 网口通信ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 
            //// await 异步发送：连接 → 发送 → 接收 三步都在 TcpAPI.Send 内部完成，
            //// 期间 UI 线程不被阻塞（不会出现窗口"未响应"）
            //await TcpAPI.Send(TotalNum.ToString(), "172.16.50.15", 10000);
            //AppendLog("通过TCP发送成功");
            //// 服务端的回传内容由 TcpAPI 写进静态字段 receiveData
            //AppendLog(TcpAPI.receiveData);
            #endregion
            #region
            await TcpClientAPI.Send(TotalNum.ToString(), "172.16.50.15", 10000);
            AppendLog(TcpClientAPI.receiveData);
            #endregion
        }
        #endregion

        #region 十四、界面事件（占位方法与菜单入口）
        /// <summary>
        /// 右侧"处理结果图像"控件（crdChuLiTu）的 Enter 事件（控件获得焦点时触发）
        /// 当前为空实现，仅保留挂点：将来若要"切进结果图就自动适配缩放"之类的交互，写在这里。
        /// 说明：Enter 在焦点切换时可能反复触发，一次操作不止一次，不要在这里写"只执行一次"的逻辑。
        /// </summary>
        private void crdChuLiTu_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 菜单【测量 → 点片】点击事件：打开工具块编辑窗体，让用户可视化地修改 VPP 里的工具与参数
        /// 流程：从单例取出工具块 → 交给 FrmToolBlock → 模态显示 → 用户在子窗体点【保存】即序列化回 VPP 文件
        /// 用 ShowDialog 而不是 Show 的原因：编辑期间必须挡住主窗体，避免主窗体那条采集链路
        /// 同时在 Run 同一个 CogToolBlock 对象（它不是线程安全的）。
        /// 注意：保存成功后必须重启主程序才生效——单例只在首次访问时反序列化一次，运行中不会重读文件。
        /// </summary>
        private void tsmiCeLiangDianPian_Click(object sender, EventArgs e)
        {
            // 把单例里的工具块实例交给编辑窗体（传的是引用，改的就是同一个对象）
            FrmToolBlock frmToolBlock = new FrmToolBlock(singleToolBlock.ToolBlock);
            // 模态显示：子窗体关闭前主窗体不可操作
            frmToolBlock.ShowDialog();
        }
        #endregion

        #region 十五、相机参数联动（菜单 → 参数子窗体 → 相机）
        /// <summary>
        /// 菜单【相机 → 修改相机参数】点击事件
        /// 流程：new 子窗体并传入FIFO → 订阅子窗体的参数变更事件 → 非模态显示
        /// 说明：子窗体要拿到 acqFifo 才能直接写相机参数，所以相机未初始化（acqFifo 为 null）时不应打开它，
        ///       实际项目里建议先判空再 new，避免子窗体下发参数时空引用。
        /// </summary>
        private void tsmiXiuGaiXiangJiCanShu_Click(object sender, EventArgs e)
        {
            // 子窗体需要FIFO才能直接改相机参数，相机未初始化时不能打开
            FrmCameraParams frmCameraParams = new FrmCameraParams(acqFifo);
            // 订阅子窗体参数变更事件，用户在子窗体确认后回调本窗体的处理方法
            frmCameraParams.ChangeCameraParams += FrmCamera_changeCameraParms;
            // 非模态显示：主窗体与子窗体可同时操作
            frmCameraParams.Show();
        }

        /// <summary>
        /// 子窗体参数确认后的回调：把 CameraParams 里的三项参数写到相机
        /// 执行时机：用户在 FrmCameraParams 点【保存设置】，子窗体触发 ChangeCameraParams 事件
        /// 说明：三项都通过 FIFO 的 Owned 系列属性下发，不需要另建相机会话
        /// </summary>
        private void FrmCamera_changeCameraParms(CameraParams cameraParams)
        {
            // 曝光时间：单位毫秒
            acqFifo.OwnedExposureParams.Exposure = cameraParams.ExposureTime;
            // 亮度（增益）：0~1归一化值
            acqFifo.OwnedBrightnessParams.Brightness = cameraParams.Brightness;
            // GigE传输延迟级别：整型档位，数值越大越抗丢包、帧率越低
            acqFifo.OwnedGigEVisionTransportParams.LatencyLevel = cameraParams.Level;
        }
        #endregion
    }
}
