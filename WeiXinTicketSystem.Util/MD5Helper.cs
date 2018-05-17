using System;
using System.Security.Cryptography;
using System.Text;

namespace WeiXinTicketSystem.Util
{
    /// <summary>
    /// MD5加密帮助类
    /// </summary>
    public class MD5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="EncryptString"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string EncryptString)
        {
            if (string.IsNullOrEmpty(EncryptString)) { throw (new Exception("密文不得为空")); }

            MD5 m_ClassMD5 = new MD5CryptoServiceProvider();

            string m_strEncrypt = "";

            try
            {
                m_strEncrypt = BitConverter.ToString(m_ClassMD5.ComputeHash(Encoding.Default.GetBytes(EncryptString))).Replace("-", "");
            }
            catch (ArgumentException ex) { throw ex; }
            catch (CryptographicException ex) { throw ex; }
            catch (Exception ex) { throw ex; }
            finally { m_ClassMD5.Clear(); }

            return m_strEncrypt;
        }
    }
}
