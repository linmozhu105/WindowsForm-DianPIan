using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.ToolBlock;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// 工具块编辑窗体
    /// 职责：把主程序里的 CogToolBlock 交给 Cognex 的编辑控件，让用户可视化地增删工具、改参数，
    ///       改完点【保存】，直接序列化回 VPP 文件。
    /// 说明：编辑控件 CogToolBlockEditV2 刻意**不在设计器里摆放**，而是在构造时用代码 new 出来。
    ///       原因：它初始化时要加载 VisionPro 运行时、工具模板和图形显示，VS 设计器里宿主它极易长时间无响应；
    ///       放在代码里创建，设计器只看到一个空 Panel，打开设计器不会再卡死。
    /// </summary>
    public partial class FrmToolBlock : Form
    {
        #region 成员字段
        /// <summary>
        /// 要编辑的工具块：由调用方传入，通常是 SingleToolBlock.Instance.ToolBlock
        /// 注意它和主程序用的是同一个对象，改完保存后仍是同一份实例
        /// </summary>
        private CogToolBlock toolBlock;

        /// <summary>
        /// Cognex 工具块编辑控件（运行时创建，停靠在下方的 panel2 里）
        /// 完整类型名：Cognex.VisionPro.ToolBlock.CogToolBlockEditV2
        /// 所在程序集：Cognex.VisionPro.ToolGroup.Controls.dll
        /// 它是一个普通 UserControl（不是 ActiveX），可以放心用 new 创建
        /// </summary>
        //private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV2;
        #endregion

        #region 构造函数
        /// <summary>
        /// 带参构造函数：必须传入要编辑的工具块，不能像设计器那样无参创建
        /// </summary>
        /// <param name="cogToolBlock">待编辑的工具块实例</param>
        public FrmToolBlock(CogToolBlock cogToolBlock)
        {
            // ① 先由设计器生成窗体骨架（panel1 放按钮、panel2 留空给编辑控件）
            InitializeComponent();


            // ⑤ 记下要编辑的工具块，并交给编辑控件显示
            toolBlock = cogToolBlock;
            cogToolBlockEditV.Subject = toolBlock;
        }
        #endregion

        #region 控件事件
        /// <summary>
        /// 【保存】按钮点击事件：把编辑控件里"当前编辑结果"存回 VPP 文件
        /// 流程：从控件取回工具块 → 交给 SingleToolBlock.SaveVpp 序列化到 程序目录\VPP\CeLiangZhouCheng.vpp
        /// 注意：保存成功后必须重启主程序才生效——单例里的工具块只在启动时加载一次，
        ///       运行中不会自动重新读文件。
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            // 从编辑控件取回工具块：用户在界面上增删工具、改参数，改的都是这个对象
            CogToolBlock toolBlock = cogToolBlockEditV.Subject;
            // 序列化保存，返回 false 表示写文件失败（路径不对、文件被占用等）
            bool result = SingleToolBlock.SaveVpp(toolBlock);
            if (result)
            {
                 MessageBox.Show("保存成功，请重启程序生效");
            }
            else
            {
                 MessageBox.Show("保存失败，请检查路径或文件占用情况");
            }
        }
        #endregion
    }
}
