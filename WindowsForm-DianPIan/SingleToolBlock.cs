using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// 工具块单例容器
    /// 作用：整个程序只反序列化一次 VPP 工具块，谁要用就通过 Instance.ToolBlock 取，避免每个窗体各加载一份。
    /// 代价：VPP 只在启动时读一次，之后改了 VPP 文件必须重启程序才生效。
    /// </summary>
    public class SingleToolBlock
    {
        #region 单例实现
        /// <summary>
        /// 单例实例：静态字段在类型初始化时（第一次访问该类时）就构造出来，
        /// 也就是说 VPP 的加载发生在"第一次有人碰 SingleToolBlock"的那一刻，而不是程序启动时。
        /// </summary>
        private static readonly SingleToolBlock _instance = new SingleToolBlock();

        /// <summary>
        /// 反序列化出来的工具块对象，对外只读暴露
        /// </summary>
        private CogToolBlock toolBlock;

        /// <summary>
        /// 私有构造函数，防止外部 new：外部只能走 Instance
        /// 【注意】这里直接按 StartupPath\VPP\CeLiangZhouCheng.vpp 加载，
        /// 文件缺失或版本不匹配会抛异常；由于发生在静态初始化里，异常会被包成
        /// TypeInitializationException，之后任何对该类的访问都直接失败（连单例都拿不到），
        /// 排查时先看内层异常（InnerException）而不是外层这条。
        /// </summary>
        private SingleToolBlock()
        {
            string vppPath = Path.Combine(Application.StartupPath, "VPP", "CeLiangZhouCheng.vpp");
            // 私有构造函数，防止外部实例化 反序列化
            toolBlock = CogSerializer.LoadObjectFromFile(vppPath) as CogToolBlock;
        }

        /// <summary>
        /// 工具块访问入口：取到的是同一个对象实例，
        /// 注意它不是线程安全的，多个线程同时 Run 需要自己加锁
        /// </summary>
        public CogToolBlock ToolBlock
        {
            get { return toolBlock; }
        }

        /// <summary>
        /// 单例访问入口：全局唯一，第一次访问时触发上面的私有构造函数
        /// </summary>
        public static SingleToolBlock Instance
        {
            get { return _instance; }
        }
        #endregion

        #region VPP 文件保存
        /// <summary>
        /// 把工具块序列化保存回 VPP 文件（覆盖 程序目录\VPP\CeLiangZhouCheng.vpp）
        /// 用途：FrmToolBlock 里用户改完工具配置后点【保存】，最终落盘就走这里。
        /// 参数来源：一般是编辑控件 CogToolBlockEditV2.Subject 回传的工具块。
        /// 返回值：true 表示写入成功；false 表示写文件失败（路径不存在、文件被杀毒/资源管理器占用、无写权限等）。
        /// 注意：① 这里不走单例，直接按固定路径覆盖写，所以保存的是"传入的那个对象"，与单例里的实例是同一个；
        ///       ② 保存成功也不会让单例重新加载，必须重启程序才能看到新配置；
        ///       ③ 捕获的是 Exception 且吞掉不记日志，排查失败原因时可临时改成把 ex.Message 弹出来。
        /// </summary>
        /// <param name="toolBlock">要保存的工具块实例</param>
        /// <returns>保存成功返回 true，失败返回 false</returns>
        public static bool SaveVpp(CogToolBlock toolBlock)
        {
            // 与单例构造函数里使用同一个路径，保证"读的"和"写的"是同一个文件
            string vppPath = Path.Combine(Application.StartupPath, "VPP", "CeLiangZhouCheng.vpp");
            bool result = false;
            try
            {
                //序列化（将对象保存永久存储二进制）
                CogSerializer.SaveObjectToFile(toolBlock, vppPath);
                result = true;
            }
            catch (Exception)
            {
                // 写失败只返回 false，由调用方决定提示文案；这里不向外抛，避免直接崩界面
                result = false;
            }
            return result;
        }
        #endregion
    }
}
