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

        public static SerialPort Send(string portName, string text)
        {
            //bool flag = false;
            SerialPort serialPort = null;
            try
            {
                //.NET Framework提供了 专门进行串口通信的类 SerialPort
                //1.创建串口通信对象
                serialPort = new SerialPort();
                //2.设置串口通信的参数
                serialPort.BaudRate = 9600;     //波特率
                serialPort.Parity = Parity.None;        //检验位
                serialPort.StopBits = StopBits.One;     //停止位
                serialPort.DataBits = 8;        //数据位
                serialPort.PortName = portName;     //串口的名字
                //3.打开串口
                serialPort.Open();
                //4.发送数据
                if (serialPort.IsOpen)
                {
                    serialPort.Write(text);
                    //flag = true;
                }
                //释放连接
                serialPort?.Close();
            }
            catch (Exception ex)
            {
                //flag = false;
                throw ex;
            }
            return serialPort;
        }
    }
}
