using System;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO.Compression;
using System.Collections.Generic;

namespace WeiXinTicketSystem.Util
{
    /// <summary>
    /// http请求处理类
    /// </summary>
    public class HttpHelper
    {
        public static CookieContainer CookieContainers = new CookieContainer();

        public static string FireFoxAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.2.23) Gecko/20110920 Firefox/3.6.23";
        public static string IE7 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; InfoPath.2; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET4.0C; .NET4.0E)";
        public static string IE8 = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; QQWubi 133; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; CIBA; InfoPath.2)";


        /// <summary>
        /// 访问url得到返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string VisitUrl(string url)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = "GET";
                req.AllowAutoRedirect = true;
                req.Timeout = 50000;
                HttpWebResponse res = (HttpWebResponse)req.GetResponse();

                Stream resst = res.GetResponseStream();

                //解压缩
                if (res.ContentEncoding.ToLower().Contains("gzip"))
                {
                    resst = new GZipStream(resst, CompressionMode.Decompress);
                }
                StreamReader sr = new StreamReader(resst, Encoding.UTF8);
                string str = sr.ReadToEnd();

                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Vista Http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string VisitUrl(string url, SortedDictionary<string, string> param)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.KeepAlive = true;
            req.Method = "GET";
            //req.ContentType = "application/json";
            req.AllowAutoRedirect = true;
            req.Timeout = 50000;

            foreach (var key in param.Keys)
            {
                req.Headers.Add(key, param[key]);
            }

            req.UserAgent = IE8;
            string result = string.Empty;
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)req.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            using (StreamReader reader = new StreamReader(res.GetResponseStream(), new UTF8Encoding()))
            {
                string str = reader.ReadToEnd();
                if (!str.Contains("message"))
                {
                    string insertStr = "\"code\":\"0\",\"message\":\"SUCCESS\",";
                    result = str.Insert(str.IndexOf("{", str.IndexOf("{") + 1) + 1, insertStr);
                }
                else
                {
                    result = "{\"data\":" + str+ "}";
                }
            }

            return result;
        }
        /// <summary>
        /// Vista Http Post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static string VisitUrl(string url, SortedDictionary<string, string> param, string paras)
        {
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
            //Post请求方式
            request.Method = "POST";
            // 内容类型
            request.ContentType = "application/x-www-form-urlencoded";

            foreach (var key in param.Keys)
            {
                request.Headers.Add(key, param[key]);
            }
            request.UserAgent = IE8;

            //这是原始代码：
            string paraUrlCoded = paras;// "p1=x&p2=y&p3=测试的中文";
            byte[] payload;
            //将URL编码后的字符串转化为字节
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);//Encoding.ASCII.GetBytes(paraUrlCoded); ;//
                                                                       //设置请求的 ContentLength 
            request.ContentLength = payload.Length;
            //获得请 求流
            Stream writer = request.GetRequestStream();
            //将请求参数写入流
            writer.Write(payload, 0, payload.Length);
            // 关闭请求流
            writer.Close();

            string result = string.Empty;
            HttpWebResponse res;
            try
            {
                res = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                res = (HttpWebResponse)ex.Response;
            }
            using (StreamReader reader = new StreamReader(res.GetResponseStream(), new UTF8Encoding()))
            {
                string str = reader.ReadToEnd();
                if(!str.Contains("data"))
                {
                    str = "{\"data\": {\"code\": \"0\",\"message\": \"SUCCESS\"}}";
                }
                else if (!str.Contains("message"))
                {
                    string insertStr = "\"code\":\"0\",\"message\":\"SUCCESS\",";
                    result = str.Insert(str.IndexOf("{", str.IndexOf("{") + 1) + 1, insertStr);
                }
                else
                {
                    result = "{\"data\":" + str + "}";
                }
            }
            return result;
        }

        /// <summary>
        /// 访问url得到返回的字符串
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string VisitUrl(string url, string paras)
        {
            try
            {
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)HttpWebRequest.Create(url);
                //Post请求方式
                request.Method = "POST";
                // 内容类型
                request.ContentType = "application/x-www-form-urlencoded";

                //这是原始代码：
                string paraUrlCoded = paras;// "p1=x&p2=y&p3=测试的中文";
                byte[] payload;
                //将URL编码后的字符串转化为字节
                payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);//Encoding.ASCII.GetBytes(paraUrlCoded); ;//
                //设置请求的 ContentLength 
                request.ContentLength = payload.Length;
                //获得请 求流
                Stream writer = request.GetRequestStream();
                //将请求参数写入流
                writer.Write(payload, 0, payload.Length);
                // 关闭请求流
                writer.Close();
                System.Net.HttpWebResponse response;
                // 获得响应流
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream resst;
                resst = response.GetResponseStream();

                //解压缩
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    resst = new GZipStream(resst, CompressionMode.Decompress);
                }
                StreamReader sr = new StreamReader(resst, Encoding.UTF8);
                string str = sr.ReadToEnd();

                return str;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return string.Empty;
            }
        }


        /// <summary>
        /// visit the target url
        /// </summary>
        /// <param name="targetURL"></param>
        /// <param name="cc">this is for keeping cookies and sessions</param>
        /// <param name="param">this is the data need post inside form</param>
        /// <returns>html page</returns>
        public static string PostAndGetHTML(string targetURL, ref CookieContainer cc, Hashtable param, string code = "gb2312")
        {
            try
            {


                //prepare the submit data
                string formData = "";
                foreach (DictionaryEntry de in param)
                {
                    formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
                }
                if (formData.Length > 0)
                    formData = formData.Substring(0, formData.Length - 1); //remove last '&'

                ASCIIEncoding encoding = new ASCIIEncoding();


                byte[] data = Encoding.GetEncoding(code).GetBytes(formData);     // encoding.GetBytes(formData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = "POST";    //post

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";


                // request.UserAgent = " Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";


                request.CookieContainer = cc;
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                request.CookieContainer = cc;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cc.Add(response.Cookies);
                Stream stream = response.GetResponseStream();



                string result = new StreamReader(stream, System.Text.Encoding.GetEncoding(code)).ReadToEnd();
                return result;
            }
            catch
            {

                return null;
            }
        }


        /// <summary>
        /// 发起http get 请求
        /// </summary>
        /// <param name="targetURL">请求路径和参数</param>
        /// <param name="cc">cookie</param>
        /// <param name="param">参数数组</param>
        /// <param name="code">编码格式</param>
        /// <param name="Format">是否需要对param进行格式化,并添加到 Url</param>
        /// <returns></returns>
        public static Stream GetStreamHTML(string targetURL, ref CookieContainer cc, Hashtable param, string code = "gb2312", bool Format = true)
        {
            try
            {

                if (Format)
                {
                    //prepare the submit data
                    string formData = "";
                    foreach (DictionaryEntry de in param)
                    {
                        formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
                    }
                    if (formData.Length > 0)
                        formData = formData.Substring(0, formData.Length - 1); //remove last '&'


                    targetURL += "?" + formData;
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = "GET";    //post 
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
                request.CookieContainer = cc;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cc.Add(response.Cookies);
                Stream stream = response.GetResponseStream();
                return stream;

            }
            catch
            {

                return null;
            }
        }
        public static string GetHTML(string targetURL, ref CookieContainer cc, string code = "gb2312")
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = "GET";    //post

                request.ContentType = "application/x-www-form-urlencoded";

                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
                request.CookieContainer = cc;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cc.Add(response.Cookies);
                Stream stream = response.GetResponseStream();
                string result = new StreamReader(stream, System.Text.Encoding.GetEncoding(code)).ReadToEnd();
                return result;
            }
            catch
            {

                return null;
            }
        }

        public static string PostFeixinHTML(string targetURL, ref CookieContainer cc, Hashtable param, string code = "utf-8", string method = "POST")
        {
            try
            {
                //prepare the submit data
                string formData = "";
                foreach (DictionaryEntry de in param)
                {
                    formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
                }
                if (formData.Length > 0)
                    formData = formData.Substring(0, formData.Length - 1); //remove last '&'

                ASCIIEncoding encoding = new ASCIIEncoding();


                byte[] data = Encoding.GetEncoding(code).GetBytes(formData);     // encoding.GetBytes(formData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = method;    //post

                request.ContentType = "application/x-www-form-urlencoded;charset=" + code;// "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.22 (KHTML, like Gecko) Chrome/25.0.1364.97 Safari/537.22";


                //"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
                request.CookieContainer = cc;
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                request.CookieContainer = cc;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                cc.Add(response.Cookies);
                Stream stream = response.GetResponseStream();
                string result = new StreamReader(stream, System.Text.Encoding.GetEncoding(code)).ReadToEnd();
                return result;
            }
            catch
            {

                return null;
            }
        }

        public static bool PostHTML(string targetURL, ref CookieContainer cc, Hashtable param, string code = "utf-8")
        {
            try
            {
                //prepare the submit data
                string formData = "";
                foreach (DictionaryEntry de in param)
                {
                    formData += de.Key.ToString() + "=" + de.Value.ToString() + "&";
                }
                if (formData.Length > 0)
                    formData = formData.Substring(0, formData.Length - 1); //remove last '&'

                ASCIIEncoding encoding = new ASCIIEncoding();


                byte[] data = Encoding.GetEncoding("gb2312").GetBytes(formData);     // encoding.GetBytes(formData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetURL);
                request.Method = "POST";    //post

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; SV1; .NET CLR 2.0.1124)";
                request.CookieContainer = cc;
                Stream newStream = request.GetRequestStream();
                newStream.Write(data, 0, data.Length);

                newStream.Close();
                return true;

            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method">"POST" or "GET"</param>
        /// <param name="data">when the method is "POST", the data will send to web server, if the method is "GET", the data should be string.empty</param>
        /// <returns></returns>
        public static string GetResponse(string url, string method, string data)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = method.ToUpper();
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = "application/x-www-form-urlencoded";

                req.UserAgent = IE7;
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Timeout = 50000;

                if (method.ToUpper() == "POST" && data != null)
                {
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    byte[] postBytes = encoding.GetBytes(data); ;
                    req.ContentLength = postBytes.Length;
                    Stream st = req.GetRequestStream();
                    st.Write(postBytes, 0, postBytes.Length);
                    st.Close();
                }

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                Encoding myEncoding = Encoding.GetEncoding("UTF-8");

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                Stream resst = res.GetResponseStream();
                StreamReader sr = new StreamReader(resst, myEncoding);
                string str = sr.ReadToEnd();

                return str;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static Stream GetResponseImage(string url)
        {
            Stream resst = null;
            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.KeepAlive = true;
                req.Method = "GET";
                req.AllowAutoRedirect = true;
                req.CookieContainer = CookieContainers;
                req.ContentType = "application/x-www-form-urlencoded";

                req.UserAgent = IE7;
                req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                req.Timeout = 50000;

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };

                Encoding myEncoding = Encoding.GetEncoding("UTF-8");

                HttpWebResponse res = (HttpWebResponse)req.GetResponse();
                resst = res.GetResponseStream();

                return resst;
            }
            catch
            {
                return null;
            }
        }

        public static string GetRegexString(string pattern, string source)
        {
            Regex r = new Regex(pattern);
            MatchCollection mc = r.Matches(source);
            return mc[0].Groups[1].Value;
        }

        public static string[] GetRegexStrings(string pattern, string source)
        {
            Regex r = new Regex(pattern);
            MatchCollection mcs = r.Matches(source);

            string[] ret = new string[mcs.Count];

            for (int i = 0; i < mcs.Count; i++)
                ret[i] = mcs[i].Groups[1].Value;

            return ret;
        }


        /// <summary>
        /// 判断Url是否存在
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static bool IsUrlExist(string Url)
        {
            bool IsExist = false;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(Url));
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)request.GetResponse()).Close();
                IsExist = true;
            }
            catch
            {
                //if (exception.Status != WebExceptionStatus.ProtocolError)
                //{
                //    return num;
                //}
                //if (exception.Message.IndexOf("500 ") > 0)
                //{
                //    return 500;
                //}
                //if (exception.Message.IndexOf("401 ") > 0)
                //{
                //    return 401;
                //}
                //if (exception.Message.IndexOf("404") > 0)
                //{
                //    num = 404;
                //}
                IsExist = false;
            }

            return IsExist;
        }


    }
}
