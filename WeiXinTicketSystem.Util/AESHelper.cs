using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WeiXinTicketSystem.Util
{
    public class AESHelper
    {
        public static string AesDecrypt(string encryptedData, string sessionKey,string iv)
        {
            if (string.IsNullOrEmpty(encryptedData))
            {
                return null;
            }
            Byte[] toEncryptArray = Convert.FromBase64String(PadRight(encryptedData));
            Byte[] keyByte = Convert.FromBase64String(sessionKey);
            Byte[] ivByte = Convert.FromBase64String(PadRight(iv));
            using (RijndaelManaged rm = new RijndaelManaged())
            {
                //var key = new Rfc2898DeriveBytes(password, salt, 10000);
                //rm.Key = key.GetBytes(KeySize / 8);
                rm.Key = keyByte;
                rm.IV = ivByte;
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
        }

        public static string PadRight(string OldData)
        {
            //过滤特殊字符即可
            string NewData = OldData.Trim().Replace("%", "").Replace(",", "").Replace(" ", "+");
            if (NewData.Length % 4 > 0)
            {
                NewData = NewData.PadRight(NewData.Length + 4 - NewData.Length % 4, '=');
            }
            return NewData;
        }
    }
}
