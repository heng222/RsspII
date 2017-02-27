using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace BJMT.RsspII4net.ITest.Utilities
{
    class HelperTools
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


        public static byte[] SplitHexText(string text)
        {
            var result = new List<byte>();
            var splitedText = text.Trim().
                Split(new string[] { "," , "，", " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in splitedText)
            {
                try
                {
                    result.Add(Convert.ToByte(item, 16));
                }
                catch (System.FormatException /*ex*/)
                {
                    throw new Exception(string.Format("无法将字符串'{0}'转化为数值。", item));
                }
            }

            return result.ToArray();
        }


        public static IPEndPoint ParseIPEndPoint(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;

            var splitedText = text.Trim().
                Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            
            var ip = IPAddress.Parse(splitedText[0]);
            int port = 0;
            if (splitedText.Length > 1)
            {
                port = int.Parse(splitedText[1]);
            }

            return new IPEndPoint(ip, port);
        }

        public static KeyValuePair<uint, List<IPEndPoint>> ParseIdAndEndPoints(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException();

            var splitedText = text.Trim().
                Split(new string[] { ",", "，"}, StringSplitOptions.RemoveEmptyEntries);

            var id = uint.Parse(splitedText[0]);

            var eps = new List<IPEndPoint>();
            for (int i = 1; i < splitedText.Length; i++)
            {
                var value = ParseIPEndPoint(splitedText[i]);
                eps.Add(value);
            }

            return new KeyValuePair<uint, List<IPEndPoint>>(id, eps);
        }
    }
}
