using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForm_DianPIan
{
    public class TcpClientAPI
    {
        public static string receiveData;
        public static async Task Send(string data, string ip, int port)
        {
            //1.创建TcpClient对象
            //IPAddress.TryParse(ip, out IPAddress address);
            //IPEndPoint ipEndPoint = new IPEndPoint(address, port);
            TcpClient tcpClient = new TcpClient(AddressFamily.InterNetwork);
            //2.建立和服务器的连接
            await tcpClient.ConnectAsync(ip, port);
            //3.获取网络流
            NetworkStream netwoikStream = tcpClient.GetStream();
            //4.发送数据流
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            netwoikStream.WriteAsync(buffer, 0, buffer.Length);

            //5.接受数据
            byte[] buffer2 = new byte[1024];
            int len = await netwoikStream.ReadAsync(buffer2, 0, buffer2.Length);
            receiveData = Encoding.UTF8.GetString(buffer2, 0, len);
        }
    }
}
