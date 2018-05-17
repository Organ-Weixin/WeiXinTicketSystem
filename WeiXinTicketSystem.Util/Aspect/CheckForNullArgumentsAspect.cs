using System;
using PostSharp.Aspects;

namespace WeiXinTicketSystem.Util
{
    /// <summary>
    /// 检查参数是否为空 Aspect
    /// </summary>
    [Serializable]
    public class CheckForNullArgumentsAspect : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            foreach (var arg in args.Arguments)
            {
                if (arg == null)
                {
                    throw new ArgumentNullException();
                }
            }
        }

    }
}
