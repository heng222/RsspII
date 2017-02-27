using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BJMT.RsspII4net.Utilities
{
    static class HelperTools
    {
        public static List<IPAddress> LocalIpAddress
        {
            get
            {
                var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                var result = new List<IPAddress>();

                IPAddress temp;
                string ip;
                for (int i = 0; i < ipHostInfo.AddressList.Length; i++)
                {
                    ip = ipHostInfo.AddressList[i].ToString();
                    if (IPAddress.TryParse(ip, out temp))
                    {
                        result.Add(temp);
                    }
                }

                return result.OrderBy(p=>p.AddressFamily).ToList();
            }
        }

        public static string ConvertToString(byte[] value)
        {
            return string.Join(", ", value.Select(p => string.Format("0x{0:X2}", p)));
        }


        public static bool ContainsEndPoint(IEnumerable<IPEndPoint> acceptableEPs, IPEndPoint clientEP)
        {
            if (acceptableEPs == null) return false;

            foreach (var item in acceptableEPs)
            {
                var portSpecified = item.Port != 0;

                if (portSpecified)
                {
                    if (item.ToString() == clientEP.ToString())
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.Address.ToString() == clientEP.Address.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsEndPointValid(IEnumerable<IPEndPoint> acceptableEPs, IPEndPoint clientEP)
        {
            if (acceptableEPs == null) return true;

            if (acceptableEPs.Count() == 0) return true;

            foreach (var item in acceptableEPs)
            {
                var portSpecified = item.Port != 0;

                if (portSpecified)
                {
                    if (item.ToString() == clientEP.ToString())
                    {
                        return true;
                    }
                }
                else
                {
                    if (item.Address.ToString() == clientEP.Address.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void SetKeepAlive(Socket socket, uint interval, uint timeout)
        {
            var buffer = new byte[12];

            BitConverter.GetBytes((uint)1).CopyTo(buffer, 0); // 启用Keep-Alive
            BitConverter.GetBytes(interval).CopyTo(buffer, 4); // 多长时间无数据包则发送Alive。（单位：毫秒）
            BitConverter.GetBytes(timeout).CopyTo(buffer, 8); // 若没有收到应答，则每隔interval再次发送。（单位：毫秒）

            socket.IOControl(IOControlCode.KeepAliveValues, buffer, null);

            //socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.co;
        }
    }
}
