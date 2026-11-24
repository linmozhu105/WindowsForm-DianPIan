using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsForm_DianPIan.MainForm;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// 检测记录数据实体
    /// 作用：一条 CSV 记录 ↔ 一个对象。既是"读文件"的载体，也是"绑表格"的数据源元素。
    /// 说明：属性名（Time / CsvCheckType / DataResult / Mark）会直接影响 DataGridView 的显示：
    ///       绑定时若开着 AutoGenerateColumns，就会按这些属性名自动建列（表头为英文）；
    ///       想用设计器里的中文表头，就给那几列设 DataPropertyName 指向这里的属性名。
    /// </summary>
    public class CsvData
    {
        #region 数据属性
        /// <summary>
        /// 时间：形如 "1124 18:07:54"（月日 时:分:秒），与 CSV 文件时间列的格式保持一致
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 检测类型：复用主窗体的 CheckType 枚举；
        /// 写 CSV 时枚举直接 ToString() 变成中文名称，读到非法文字时 TryParse 会落到默认值
        /// </summary>
        public CheckType CsvCheckType { get; set; }

        /// <summary>
        /// 检测结果：OK / NG，界面据此着色（绿 / 红）
        /// </summary>
        public string DataResult { get; set; }

        /// <summary>
        /// 备注：产品名、工位名等补充信息
        /// </summary>
        public string Mark { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 无参构造函数
        /// 用途：BindingList 里新增空对象、设计器反射、将来做反序列化时都需要它
        /// </summary>
        public CsvData() { }

        /// <summary>
        /// 四参构造函数：把"一行 CSV"的四个字段一次性装进对象
        /// 调用场景：ReadCsv 逐行解析时、以及运行时新增一条检测记录时
        /// </summary>
        /// <param name="time">时间文本</param>
        /// <param name="csvCheckType">检测类型</param>
        /// <param name="dataResult">检测结果 OK / NG</param>
        /// <param name="mark">备注</param>
        public CsvData(string time, CheckType csvCheckType, string dataResult, string mark)
        {
            Time = time;
            CsvCheckType = csvCheckType;
            DataResult = dataResult;
            this.Mark = mark;
        }
        #endregion
    }
}
