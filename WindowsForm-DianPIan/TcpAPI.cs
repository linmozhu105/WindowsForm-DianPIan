using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_DianPIan
{
    /// <summary>
    /// TCP 通信工具类（客户端）
    /// 用途：把检测数据发给服务端的上位机 / MES 系统，并接收一次回传内容。
    /// 实现方式：System.Net.Sockets.Socket + async/await，连接、发送、接收全部异步，不阻塞界面。
    /// 使用前要知道：
    ///   ① IP 与端口必须与监听端一致，且服务端程序要先启动，否则 ConnectAsync 会抛
    ///      SocketException（目标计算机积极拒绝 / 连接超时）；
    ///   ② 回传内容放在静态字段 receiveData 里，属于"全局变量"写法，多处同时调用会互相覆盖；
    ///   ③ 每次调用都新建 Socket，但结束时没有 Close/Dispose——句柄会一直占着，
    ///      长跑程序应改成 using (Socket socket = ...) 或在 finally 里 socket.Close()。
    /// </summary>
    public static class TcpAPI
    {
        #region 一、字段
        /// <summary>
        /// 服务端最近一次回传的文本（UTF-8 解码后）
        /// 注意：静态字段，全类共享；Send 执行完返回后调用方才读得到本次结果
        /// </summary>
        public static string receiveData;
        #endregion

        #region 二、发送数据（TCP 客户端）
        /// <summary>
        /// 向指定 IP:端口 发送文本，并等待一次回传
        /// 流程：建 Socket → 解析 IP → ConnectAsync → SendAsync → ReceiveAsync → 解码存入 receiveData
        /// 返回值：Task，调用方必须 await（事件处理器则写成 async void 再 await），否则数据发不出去
        /// 注意：① ReceiveAsync 会一直等到对端发数据或断开，对端不回复时调用方会长时间挂起；
        ///       ② IP 解析失败时 serverIP 为 null，构造 IPEndPoint 会抛 ArgumentNullException；
        ///       ③ Socket 用完没有释放，详见类注释第 ③ 条。
        /// </summary>
        /// <param name="data">要发送的文本，内部按 UTF-8 编码成字节</param>
        /// <param name="IP">服务端 IP，形如 "172.16.50.15"</param>
        /// <param name="port">服务端端口，需与监听端一致</param>
        public static async Task Send(string data, string IP, int port)
        {
            #region 旧方案：同步 Socket（整体注释，保留作对照）
            ////客户端发送数据到服务端
            ////1.声明了套接字对象 
            //Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ////2.发送连接请求
            //IPAddress.TryParse(IP, out IPAddress serverIP);//"172.16.50.15",IP
            //socket.Connect(serverIP, 10000);//port
            //byte[] buffer = Encoding.UTF8.GetBytes(data);
            ////3.发送
            //socket.Send(buffer);
            #endregion

            #region 当前方案：异步连接 → 发送 → 接收
            //客户端发送数据到服务端
            //1.声明了套接字对象 
            // Socket(地址族, 套接字类型, 协议类型) 三个参数的含义：
            //   AddressFamily.InterNetwork —— 使用 IPv4 地址（IPv6 要写 InterNetworkV6）
            //   SocketType.Stream          —— 流式套接字，对应 TCP 的"字节流、按序到达、不丢不重"
            //   ProtocolType.Tcp           —— 明确指定 TCP 协议；流式套接字配 Tcp 是固定搭配
            // 三者写错任一个都会在 new 这一步就抛 ArgumentException，属于"配错就建不出来"
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2.发送异步连接请求
            // IPAddress.TryParse(字符串, out 结果)：把 "172.16.50.15" 这种文本解析成 IPAddress 对象
            //   返回 bool 表示是否解析成功；out 表示"由方法内部给这个变量赋值"，
            //   所以可以就地声明 serverIP（C# 7 的 out 变量语法，变量作用域延伸到外层）
            //   注意：解析失败时返回 false 且 serverIP 为 null，下一行 new IPEndPoint 会抛 ArgumentNullException
            IPAddress.TryParse(IP, out IPAddress serverIP);//"172.16.50.15",IP

            // IPEndPoint 代表网络上的一个"通信端点"，即 IP + 端口号，
            // 相当于给数据包写明收件地址：哪台机器的哪个程序
            IPEndPoint iPEndPoint = new IPEndPoint(serverIP, port);

            // ConnectAsync：异步发起 TCP 三次握手
            //   await 的含义是"等这个异步操作完成后再执行下一行"，但等待期间当前线程会被释放回线程池，
            //   所以界面不会卡死；对比同步版 socket.Connect()，网络不通时会整整卡住界面直到超时
            //   连接失败（服务端没启动、端口没监听、防火墙拦截）会抛 SocketException
            await socket.ConnectAsync(iPEndPoint);
            // 把字符串按 UTF-8 编码成字节数组：网络上只能传字节，不能直接传 string
            //   选 UTF-8 就要和对端约定一致，对端若按 GBK 解码，中文会变成乱码
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            // SendAsync(数据段, 发送标志)：
            //   ArraySegment<byte>(buffer) 表示"把整个 buffer 当成一段数据"，是为兼容老框架异步 API 的写法
            //   （.NET Framework 4.5 的 Socket 异步方法只接受 ArraySegment，不能直接传 byte[]）
            //   SocketFlags.None 表示不带特殊标志：不延迟合并发送、不带外带数据（OOB）
            await socket.SendAsync(new ArraySegment<byte>(buffer),SocketFlags.None);
            // 准备接收缓冲区：一次最多收 1024 字节
            //   若对端回传内容超过 1024 字节，会被截断，需要循环接收才能收全
            byte[] recriverBuffer = new byte[1024];
            // ReceiveAsync 的返回值 len 是"本次实际收到的字节数"，不是缓冲区长度
            //   缓冲区没被填满的部分全是 0，所以下面只能解码前 len 个字节
            //   若对端既不回数据也不断开连接，这一行会一直等下去（没有超时机制）
            int len = await socket.ReceiveAsync(new ArraySegment<byte>(recriverBuffer), SocketFlags.None);
            // GetString(字节数组, 起始索引, 读取长度)：只解码 [0, len) 这一段有效数据
            //   写成 GetString(recriverBuffer) 会把后面 1024-len 个 0 字节也解出来，末尾出现一堆 \0
            receiveData = Encoding.UTF8.GetString(recriverBuffer, 0, len);

            #endregion

        }
        #endregion

        #region 备用：SocketAsyncEventArgs 回调方式（已注释，未使用）
        //private static void SocketAsyncEventArgs_Completed(object sender, SocketAsyncEventArgs e)
        //{
        //    Socket socket = sender as Socket;
        //    byte[] buffer = new byte[1024];
        //    socket.Receive(buffer);
        //    string receiveData = Encoding.UTF8.GetString(buffer);
        //}
        #endregion
    }
}