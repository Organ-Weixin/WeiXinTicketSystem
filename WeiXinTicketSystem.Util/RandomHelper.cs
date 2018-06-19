using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace WeiXinTicketSystem.Util
{
    public class RandomHelper
    {
        /// <summary>
        /// 生成指定位数的随机密码(最简单的六位数随机码)
        /// </summary>
        /// <param name="sum">默认是6</param>
        /// <returns></returns>
        public static string CreatePwd(int sum = 6)
        {
            Random x = new Random();
            string p = "";
            for (int i = 0; i < sum; i++)
            {
                p += x.Next(1, 9);
            }
            return p;
        }
        /// <summary>
        /// 8位字母数字混合随机码
        /// </summary>
        /// <returns></returns>
        public static string CreateRandomCode()
        {
            List<char> chars = new List<char>(62);
            for (int i = 0; i < 10; i++)
                chars.Add((char)('0' + i));
            for (int i = 0; i < 26; i++)
                chars.Add((char)('A' + i));
            for (int i = 0; i < 26; i++)
                chars.Add((char)('a' + i));
            string result = "";
            int index;
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                index = r.Next(chars.Count);
                result += chars[index].ToString();
                chars.RemoveAt(index);
            }
            return result;
        }
        /// <summary>
        /// 生成16位订单号
        /// </summary>
        private static long np1 = 0, np2 = 0, np3 = 1; //临时计算用。
        private static object orderFormNumberLock = new object();//线程并行锁，以保证同一时间点只有一个用户能够操作流水号。如果分多个流水号段，放多个锁，并行压力可以更好的解决，大家自己想法子扩充吧
        public static string InitializeOrderNumber()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = now - DateTime.MinValue;
            long tmpDays = span.Days;
            long seconds = span.Hours * 3600 + span.Seconds;
            StringBuilder sb = new StringBuilder();
            Monitor.Enter(orderFormNumberLock); //锁定资源
            if (tmpDays != np1)
            {
                np1 = tmpDays;
                np2 = 0;
                np3 = 1;
            }
            if (np2 != seconds)
            {
                np2 = seconds;
                np3 = 1;
            }
            sb.Append(Convert.ToString(np1, 16).PadLeft(5, '0') + Convert.ToString(np2, 16).PadLeft(5, '0') + Convert.ToString(np3++, 16).PadLeft(6, '0'));
            Monitor.Exit(orderFormNumberLock); //释放资源
            return sb.ToString();
        }
    }
}
