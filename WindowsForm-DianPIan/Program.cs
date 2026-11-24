using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm_DianPIan
{
    internal static class Program
    {
        #region 程序入口
        /// <summary>
        /// 应用程序的主入口点
        /// [STAThread]：把主线程初始化为单线程单元（STA），这是 WinForms + ActiveX（CogRecordDisplay）的硬性要求，
        /// 去掉会报"当前线程不在单线程单元中，因此无法实例化 ActiveX 控件"。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 启用操作系统风格的视觉样式（按钮、分组框等按主题绘制）
            Application.EnableVisualStyles();
            // 文本渲染方式：false 表示用 GDI 渲染，兼容老控件；true 走 GDI+，文字更圆润但个别控件会错位
            Application.SetCompatibleTextRenderingDefault(false);
            // 启动主消息循环并显示主窗体，这一句阻塞到主窗体关闭为止
            Application.Run(new MainForm());
        }
        #endregion
    }
}
