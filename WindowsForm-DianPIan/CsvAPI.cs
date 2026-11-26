using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsForm_DianPIan.MainForm;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// CSV 记录工具类
    /// 职责：把每一次检测的结果追加写入"程序目录\Data\年月.csv"，一个自然月对应一个文件。
    /// 设计要点：
    ///   ① 只有当文件里还一个字都没有时才写表头，避免每次追加都重复写一行表头；
    ///   ② 用 UTF-8 编码写入，Excel / WPS 直接双击就能正确显示中文；
    ///   ③ 本类只负责"落盘"，不负责界面提示；写失败（IOException）时由调用方决定怎么记日志。
    /// 常见故障：CSV 文件正被 Excel / WPS 打开时会被独占锁定，此时写入抛 IOException
    ///           （提示"正由另一进程使用，因此该进程无法访问此文件"），这是外部占用，关掉表格软件即可。
    /// </summary>
    public class CsvAPI
    {
        #region 常量与静态初始化

        /// <summary>
        /// 本月 CSV 文件的完整路径，形如 程序目录\Data\202611.csv
        /// 由静态构造函数在类型首次使用时算好，后续 读 / 写 共用同一个路径，保证两边指向同一个文件。
        /// 注意：只在程序启动时取一次年月，程序跨月不重启的话仍然写在上个月的文件里；
        ///       若需要严格按月分文件，应改成每次调用时重新按 DateTime.Now 拼路径。
        /// </summary>
        static string csvPath;


        /// <summary>
        /// 静态构造函数：本类型第一次被使用时自动执行，且只执行一次
        /// 作用：确保 程序目录\Data 目录存在，并算好本月 CSV 文件路径
        /// 注意：Application.StartupPath 依赖 WinForms，所以本类只能在 WinForms 程序里用
        /// </summary>
        static CsvAPI()
        {
            // 拼出 Data 目录的绝对路径：程序运行目录\Data
            string filePath = Path.Combine(Application.StartupPath, "Data");
            // 目录不存在才创建；CreateDirectory 本身对已存在目录是幂等的，这里的判断只是为了语义清晰
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            // 按"年月"确定本月文件（如 202611.csv），读和写都用这一个 csvPath
            DateTime dateTime = DateTime.Now;
            csvPath = Path.Combine(Application.StartupPath, "Data", $"{dateTime.ToString("yyyyMM")}.csv");
        }
        #endregion

        #region 写入检测记录
        /// <summary>
        /// 追加一行检测记录到"当前年月.csv"
        /// 执行流程：确定本月文件路径 → 判断是否需要表头 → 追加模式打开文件 → 必要时先写表头 → 写数据行
        /// </summary>
        /// <param name="type">检测类型（缺陷检测 / 测量 / 识别 / 引导），写入"检测类型"列</param>
        /// <param name="result">检测结果，通常是 OK 或 NG，写入"检测结果"列</param>
        /// <param name="mark">备注，写产品名、工位名等补充信息，写入"备注"列</param>
        public static void SaveCsv(CheckType type, string result, string mark)
        {
            // 取当前时间：既用来确定文件名（年月），也用来写"时间"列，保证文件名与内容取自同一时刻
            DateTime dateTime = DateTime.Now;
            // 是否需要写表头：文件不存在，或者文件长度为 0（刚建出来的空文件），都算"还没有表头"
            // 说明：这里在打开 StreamWriter 之前判断，读的是磁盘上的真实状态，不会被本次写入影响；
            //       || 是短路运算，文件不存在时不会再去取 Length（对不存在的文件取 Length 会抛异常）
            bool needHeader = !File.Exists(csvPath) || new FileInfo(csvPath).Length == 0;

            // append=true：以追加方式打开，文件不存在会自动创建（含目录已由静态构造函数保证）
            // using：离开作用域时自动 Dispose，保证文件句柄一定释放，不会把自己锁住
            using (StreamWriter sw = new StreamWriter(csvPath, true, Encoding.UTF8))
            {
                // 只有"本月第一次写"才补表头，之后每次追加都跳过，避免表头出现在文件中间
                if (needHeader)
                {
                    sw.WriteLine("时间,检测类型,检测结果,备注");
                }
                // 写数据行，四列顺序与表头一一对应
                // 时间列用 "MM/dd HH:mm:ss"：只写到"日"，年、月已经体现在文件名里，不必重复
                sw.WriteLine($"{dateTime.ToString("dd HH:mm:ss")},{type},{result},{mark}");
            }
        }
        #endregion

        #region 读取检测记录
        /// <summary>
        /// 读取本月 CSV 文件里的全部检测记录
        /// 返回：BindingList&lt;CsvData&gt;，每个元素是一行记录；文件不存在时返回 null（由调用方提示"无数据"）
        /// 流程：打开文件 → 跳过第一行表头 → 逐行按逗号拆分 → 转成 CsvData → 加入列表
        /// 注意：① 这是"全量读取"，文件行数多时耗时会变长，只会在大文件上明显；
        ///       ② 返回 BindingList 是为了直接绑给 DataGridView，增删元素会自动通知界面刷新；
        ///       ③ 只按逗号拆分，若备注里本身含逗号会导致列数变多、后面取值错位（见下方长度判断）。
        /// </summary>
        public static BindingList<CsvData> ReadCsv()
        {
            // 文件不存在（本月还没有任何检测记录）：返回 null，调用方按"无数据"处理
            if (!File.Exists(csvPath))
            {
                return null;
            }

            // 准备结果容器：BindingList 在元素增减时会发通知，绑到 DataGridView 后界面自动更新
            BindingList<CsvData> list = new BindingList<CsvData>();
            try
            {
                // using：读完自动释放文件句柄，避免把文件锁住影响后续写入
                using (StreamReader sr = new StreamReader(csvPath, Encoding.UTF8))
                {
                    // 第一行是表头，读掉不用
                    string header = sr.ReadLine();
                    string temoStr = null;
                    // ReadLine 返回 null 表示到了文件末尾，循环结束
                    while ((temoStr = sr.ReadLine()) != null)
                    {
                        // 按逗号拆成"时间 / 检测类型 / 检测结果 / 备注"四段
                        string[] splitStr = temoStr.Split(',');
                        // 检测类型存的是中文枚举名（如"缺陷检测"），TryParse 失败时 checkType 为默认值（0 缺陷检测）
                        Enum.TryParse<CheckType>(splitStr[1], out CheckType checkType);

                        // 组装成一条记录，稍后统一加入列表
                        CsvData data = new CsvData(splitStr[0], checkType, splitStr[2], splitStr[3]);
                        list.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                // 读文件异常（被占用、编码不符、字段不足导致索引越界）统一弹窗提示
                // 注意：MessageBox 是模态的，会阻塞界面等待用户点确定；正式项目建议改成 AppendLog 记一行日志
                MessageBox.Show(ex.Message);
            }
            // 即使中途出错也把已读到的部分返回，不影响界面先显示已有数据
            return list;

        }
        #endregion
    }
}
