using Elmah;
using System;
using System.Web;

namespace WeiXinTicketSystem.Utils
{
    public class LogHelper
    {
        public static void LogMessage(string message)
        {
            LogException(ex: null, contextualMessage: message);
        }

        public static void LogMessage(string format, params object[] args)
        {
            LogException(ex: null, contextualMessage: string.Format(format, args));
        }

        public static void LogException(Exception ex, string format, params object[] args)
        {
            LogException(ex, string.Format(format, args));
        }

        public static void LogException(Exception ex, string contextualMessage = null)
        {
            if (ex == null && contextualMessage == null)
            {
                throw new ArgumentNullException("ex or contextualMessage is null");
            }

            try
            {
                // log error to Elmah
                if (contextualMessage != null && ex != null)
                {
                    // log exception with contextual information that's visible when 
                    // clicking on the error in the Elmah log
                    var annotatedException = new Exception(contextualMessage, ex);
                    ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
                }
                else if (ex != null)
                {
                    ErrorSignal.FromCurrentContext().Raise(ex, HttpContext.Current);
                }
                else if (contextualMessage != null)
                {
                    var annotatedException = new Exception(contextualMessage);
                    ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
                }

                // send errors to ErrorWS (my own legacy service)
                // using (ErrorWSSoapClient client = new ErrorWSSoapClient())
                // {
                //    client.LogErrors(...);
                // }
            }
            catch (Exception)
            {
                // uh oh! just keep going
            }
        }
    }
}