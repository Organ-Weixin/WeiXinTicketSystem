using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Util
{
    public class WxPayUtil
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        /// <returns></returns>
        public static string getTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        /// <summary>
        /// 随机字符串
        /// </summary>
        /// <returns></returns>
        public static string getNoncestr()
        {
            Random random = new Random();
            return MD5Helper.GetMD5(random.Next(1000).ToString(), "GBK");
        }
    }
}
