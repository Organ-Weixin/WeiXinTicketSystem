using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXinTicketSystem.Entity.Models;

namespace WeiXinTicketSystem.Models
{
    public class SessionInfoResponse
    {
        public List<SessionInfoEntity> SessionInfos { get; set; }
    }
}