using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// 串口通信工具类
    /// 用途：把一段文本（例如检测结果 OK / NG）通过串口发给下位机——PLC、报警灯、分拣机构、扫码枪等。
    /// 底层：使用 .NET Framework 自带的 System.IO.Ports.SerialPort，不需要第三方库。
    /// 设计取向：每次都"新建对象 → 打开 → 写完 → 关闭"，用完即弃，不维持长连接，
    ///           好处是不会忘记关端口，坏处是每次发送都要重新握手，延迟较高、也无法接收回传数据。
    /// 使用前必须知道的三件事：
    ///   ① 串口号（COM1 / COM3…）要以设备管理器里实际显示的为准，插拔 USB 转串口后可能变号；
    ///   ② 波特率 / 校验位 / 停止位 / 数据位四项必须与下位机完全一致，否则发出去是乱码甚至收不到；
    ///   ③ 串口是独占资源，同一端口被别的程序（串口助手、上位机软件）打开时，这里 Open 会抛
    ///      UnauthorizedAccessException（提示"拒绝访问"）。
    /// </summary>
    public static class ComPortAPI
    {
        #region 串口发送
        /// <summary>
        /// 向指定串口发送一段文本
        /// 流程：创建对象 → 设置五项通信参数 → Open → 判断已打开后 Write → Close
        /// 参数全是硬编码（9600 / None / One / 8），换设备时必须改这里，或者改造成由参数传入。
        /// 注意：① 返回值是"已经 Close 掉的 SerialPort"，调用方拿到它也无法再收发数据，
        ///          因此下面这种写法（本项目 MainForm 里注释掉的那种）是无效的：
        ///          var sp = ComPortAPI.Send(...); sp.DataReceived += ...;  // 端口已关，事件永不触发
        ///       ② catch 里用 throw;（不带异常变量）能保留原始堆栈；写成 throw ex; 会把堆栈重置到 catch 这一行，
        ///          排查时看不到真正出错的位置；
        ///       ③ 只 Close 没 Dispose，长跑程序反复调用建议补 using 或 finally 里 Dispose。
        /// </summary>
        /// <param name="portName">串口名，形如 "COM1"、"COM3"</param>
        /// <param name="text">要发送的文本；中文需与下位机约定的编码一致（这里 SerialPort 默认按 ASCII/UTF-8 处理）</param>
        /// <returns>已经关闭的 SerialPort 对象，调用方一般不需要使用，建议将来改为返回 bool 表示是否发送成功</returns>
        public static SerialPort Send(string portName, string text)
        {
            //bool flag = false;
            SerialPort serialPort = null;
            try
            {
                //.NET Framework提供了 专门进行串口通信的类 SerialPort
                //1.创建串口通信对象
                // new SerialPort() 只是建对象，此时还没占用任何端口，端口号与参数都还没生效
                serialPort = new SerialPort();
                //2.设置串口通信的参数
                // 这四项（波特率、校验位、停止位、数据位）加端口号，合称"串口五参数"，
                // 必须与下位机完全一致，差一项收到的就是乱码或干脆无响应
                serialPort.BaudRate = 9600;             //波特率：每秒传输的位数，常用 9600 / 19200 / 115200
                serialPort.Parity = Parity.None;        //校验位：None 表示不做奇偶校验（最常用）
                serialPort.StopBits = StopBits.One;     //停止位：标志一个字节结束，One 表示 1 位停止位
                serialPort.DataBits = 8;                //数据位：一个字符用几位传，8 位正好一个字节
                serialPort.PortName = portName;         //串口名：形如 COM1 / COM3，由方法参数传入
                //3.打开串口
                // Open 才真正向系统申请独占端口：端口不存在抛 ArgumentException（无效端口名），
                // 已被别的程序占用则抛 UnauthorizedAccessException（拒绝访问）
                serialPort.Open();
                //4.发送数据
                // IsOpen 判断只是防御性写法：上一步 Open 成功才会走到这里，失败会直接跳到 catch
                if (serialPort.IsOpen)
                {
                    serialPort.Write(text);
                    //flag = true;
                }
                //释放连接
                // Close 会释放串口句柄，之后同一端口可被其它程序打开
                // serialPort?.Close() 里的 ?. 是"空条件运算符"：serialPort 为 null 时整句直接跳过、不抛异常。
                //   为什么需要它：上面的 new SerialPort() 一旦失败，serialPort 仍是 null，
                //   此时写 serialPort.Close() 会抛 NullReferenceException，把真正的异常盖掉
                serialPort?.Close();
            }
            catch (Exception)
            {
                //flag = false;
                // 把异常原样抛给上层，由调用方决定提示还是记日志
                // 用 throw;（不带异常变量）可以保留原始堆栈，排查时能看到真正出错的位置；
                // 写成 throw ex; 会把堆栈重置到这一行，等于丢掉了现场信息（RCS104 提示的就是这个）
                throw;
            }
            return serialPort;
        }
        #endregion
    }
}
