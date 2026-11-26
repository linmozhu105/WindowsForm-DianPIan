using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsForm_DianPIan
{
    public static class ComPortAPI
    {

        public static bool Send(string portName, string text)
        {
            bool flag = false;
            try
            {
                //.NET Framework提供了 专门进行串口通信的类 SerialPort
                //1.创建串口通信对象
                SerialPort seroalPort = new SerialPort();
                //2.设置串口通信的参数
                seroalPort.BaudRate = 9600;     //波特率
                seroalPort.Parity = Parity.None;        //检验位
                seroalPort.StopBits = StopBits.One;     //停止位
                seroalPort.DataBits = 8;        //数据位
                seroalPort.PortName = portName;     //串口的名字
                //3.打开串口
                seroalPort.Open();
                //4.发送数据
                if (seroalPort.IsOpen)
                {
                    seroalPort.Write(text);
                    flag = true;
                }
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
            return flag;
        }
    }
}
