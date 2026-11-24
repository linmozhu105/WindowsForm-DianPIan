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
            // 私有构造函数，防止外部实例化
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
    }
}
