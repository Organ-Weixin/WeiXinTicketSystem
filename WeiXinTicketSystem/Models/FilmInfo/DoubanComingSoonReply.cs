using WeiXinTicketSystem.Entity.Enum;
using WeiXinTicketSystem.Entity.Models;
using WeiXinTicketSystem.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WeiXinTicketSystem.Models
{
    public class DoubanComingSoonReply
    {
        public int count { get; set; }
        public int start { get; set; }
        public int total { get; set; }
        public List<subject> subjects { get; set; }
        public string title { get; set; }
    }
    public class subject
    {
        public rating rating { get; set; }
        public string[] genres { get; set; }
        public int collect_count { get; set; }
        public List<cast> casts { get; set; }
        public string title { get; set; }
        public string original_title { get; set; }
        public string subtype { get; set; }
        public List<director> directors { get; set; }
        public string year { get; set; }
        public images images { get; set; }
        public string alt { get; set; }
        public string id { get; set; }


    }
    public class rating
    {
        public int max { get; set; }
        public float average { get; set; }
        public string stars { get; set; }
        public int min { get; set; }
    }
    public class cast
    {
        public avatars avatars { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
    public class director
    {
        public avatars avatars { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
    public class images
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }

    }
    public class avatars
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }
}