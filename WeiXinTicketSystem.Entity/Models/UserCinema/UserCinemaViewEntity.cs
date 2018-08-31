using System.Text.RegularExpressions;

namespace WeiXinTicketSystem.Entity.Models
{
    public partial class UserCinemaViewEntity
    {
        /// <summary>
        /// 接入商所使用的账号
        /// </summary>
        public string RealUserName
        {
            get
            {
                return string.IsNullOrEmpty(UserName) ? DefaultUserName : UserName;
            }
        }

        /// <summary>
        /// 接入商所使用的密码
        /// </summary>
        public string RealPassword
        {
            get
            {
                return string.IsNullOrEmpty(Password) ? DefaultPassword : Password;
            }
        }

        /// <summary>
        /// 国标取票IP
        /// </summary>
        public string FetchTicketIp
        {
            get
            {
                Regex regex = new Regex(@"\d+\.\d+\.\d+\.\d+");
                Match match = regex.Match(Url);
                if (match.Success)
                {
                    return match.Value;
                }
                return string.Empty;
            }
        }
    }
}
