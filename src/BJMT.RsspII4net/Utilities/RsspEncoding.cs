/*----------------------------------------------------------------
// 公司名称：北京交大微联科技有限公司
// 
// 项目名称：BJMT Platform Library
//
// 创 建 人：zhangheng
// 创建日期：2016-11-17 11:08:52 
// 邮    箱：zhangheng@bjmut.com
//
// Copyright (C) 北京交大微联科技有限公司，保留所有权利。
//
//----------------------------------------------------------------*/
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace BJMT.RsspII4net.Utilities
{
    /// <summary>
    /// 编码系统
    /// </summary>
    public static class RsspEncoding
    {
        #region "Filed"
        /// <summary>
        /// 通讯协议中传输字符串时使用的编码方案
        /// </summary>
        private readonly static Encoding _charsEncoding = Encoding.UTF8;
        #endregion

        #region "Public methods"
        /// <summary>
        /// 将主机字符串序列化为网络上传输的格式。
        /// </summary>
        /// <param name="hostString">包含要编码的字符的 System.String。</param>
        /// <param name="length">期望的数组长度，-1表示无限制</param>
        /// <returns>一个字节数组，包含对指定的字符串进行编码的结果。</returns>
        public static byte[] ToNetString(string hostString, int length = -1)
        {
            var bytes = _charsEncoding.GetBytes(hostString);
            if (length == -1 || bytes.Length == length)
            {
                return bytes;
            }
            else if (bytes.Length < length)
            {
                var stream = new byte[length];
                bytes.CopyTo(stream, 0);
                return stream;
            }
            else
            {
                return bytes.Take(length).ToArray();
            }
        }
        /// <summary>
        /// 将通讯网络上传输的字符串解码为本地字符串。
        /// </summary>
        /// <param name="netBytes">通讯协议中的字节流</param>
        /// <param name="index">第一个要解码的字节的索引</param>
        /// <param name="len">要解码的字节数</param>
        /// <returns>一个本地字符串</returns>
        public static string ToHostString(byte[] netBytes, int index, int len)
        {
            if (netBytes == null || netBytes.Length == 0) return string.Empty;

            if ((len + index) > netBytes.Length)
            {
                throw new ArgumentException("指定的解码长度无效。");
            }

            var result = _charsEncoding.GetString(netBytes, index, len);
            return result.Trim('\0');
        }

        /// <summary>
        /// 将Int16值序列化为网络上传输的格式。
        /// </summary>
        /// <param name="hostValue">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的短值</returns>
        public static byte[] ToNetInt16(Int16 hostValue)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(hostValue));
        }
        /// <summary>
        /// 将网络上传输的Int16数据解码为本机使用的数值。
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的短值</returns>
        public static short ToHostInt16(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt16(value, startIndex));
        }

        /// <summary>
        /// 将无符号短值由主机字节顺序转换为通讯协议字节顺序
        /// </summary>
        /// <param name="host">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的短值</returns>
        public static byte[] ToNetUInt16(UInt16 host)
        {
            return ToNetInt16((Int16)host);
        }
        /// <summary>
        /// 将通讯协议字节流转换成主机顺序表示的短值
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的短值</returns>
        public static UInt16 ToHostUInt16(byte[] value, int startIndex)
        {
            return (UInt16)ToHostInt16(value, startIndex);
        }


        /// <summary>
        /// 将整数值由主机字节顺序转换为通讯协议字节顺序
        /// </summary>
        /// <param name="host">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的整数值</returns>
        public static byte[] ToNetInt32(int host)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
        }
        /// <summary>
        /// 将通讯协议字节流转换成主机顺序表示的整数值
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的整数值</returns>
        public static int ToHostInt32(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt32(value, startIndex));
        }

        /// <summary>
        /// 将整数值由主机字节顺序转换为通讯协议字节顺序
        /// </summary>
        /// <param name="host">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的整数值</returns>
        public static byte[] ToNetUInt32(UInt32 host)
        {
            return ToNetInt32((Int32)host);
        }
        /// <summary>
        /// 将通讯协议字节流转换成主机顺序表示的整数值
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的整数值</returns>
        public static UInt32 ToHostUInt32(byte[] value, int startIndex)
        {
            return (UInt32)ToHostInt32(value, startIndex);
        }

        /// <summary>
        /// 将长值由主机字节顺序转换为通讯协议字节顺序
        /// </summary>
        /// <param name="host">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的长值</returns>
        public static byte[] ToNetInt64(Int64 host)
        {
            return BitConverter.GetBytes(IPAddress.HostToNetworkOrder(host));
        }
        /// <summary>
        /// 将通讯协议字节流转换成主机顺序表示的长值
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的长值</returns>
        public static long ToHostInt64(byte[] value, int startIndex)
        {
            return IPAddress.NetworkToHostOrder(BitConverter.ToInt64(value, startIndex));
        }

        /// <summary>
        /// 将长值由主机字节顺序转换为通讯协议字节顺序
        /// </summary>
        /// <param name="host">以主机字节顺序表示的要转换的数字</param>
        /// <returns>以网络字节顺序表示的长值</returns>
        public static byte[] ToNetUInt64(UInt64 host)
        {
            return ToNetInt64((Int64)host);
        }
        /// <summary>
        /// 将通讯协议字节流转换成主机顺序表示的长值
        /// </summary>
        /// <param name="value">通讯协议中的字节数组</param>
        /// <param name="startIndex">value 内的起始位置。</param>
        /// <returns>以主机字节顺序表示的长值</returns>
        public static UInt64 ToHostUInt64(byte[] value, int startIndex)
        {
            return (UInt64)ToHostInt64(value, startIndex);
        }

        #endregion

    }
}
