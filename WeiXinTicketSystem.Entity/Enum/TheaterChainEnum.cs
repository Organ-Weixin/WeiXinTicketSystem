using System.ComponentModel;
using System.Xml.Serialization;

namespace WeiXinTicketSystem.Entity.Enum
{
    /// <summary>
    /// 院线枚举
    /// </summary>
    public enum TheaterChainEnum:byte
    {
        [XmlEnum("1")]
        [Description("万达院线")]
        WanDa = 1,

        [XmlEnum("2")]
        [Description("大地院线")]
        Dadi = 2,

        [XmlEnum("3")]
        [Description("上海联和院线")]
        LianHe = 3,

        [XmlEnum("4")]
        [Description("中影星美院线")]
        ZhongYingXingMei = 4,

        [XmlEnum("5")]
        [Description("中影南方新干线")]
        XinganXian = 5,

        [XmlEnum("6")]
        [Description("中数院线")]
        ZhongShu = 6,

        [XmlEnum("7")]
        [Description("金逸珠江院线")]
        JinYi = 7,

        [XmlEnum("8")]
        [Description("横店院线")]
        HengDian = 8,

        [XmlEnum("9")]
        [Description("时代院线")]
        Shida = 9,

        [XmlEnum("10")]
        [Description("华夏联合院线")]
        Huaxia = 10,

        [XmlEnum("11")]
        [Description("江苏幸福蓝海院线")]
        Xinfulanhai = 11,

        [XmlEnum("12")]
        [Description("四川太平洋")]
        Taipingyang = 12,

        [XmlEnum("13")]
        [Description("北京新影联院线")]
        Xinyinglian = 13,

        [XmlEnum("14")]
        [Description("保利万和院线")]
        Wanhe = 14,

        [XmlEnum("15")]
        [Description("时代华夏今典院线")]
        Jingdian = 15,

        [XmlEnum("16")]
        [Description("深影橙天院线")]
        Chengtian = 16,

        [XmlEnum("17")]
        [Description("湖北银兴院线")]
        Yinxing = 17,

        [XmlEnum("18")]
        [Description("河南奥斯卡")]
        Aosika = 18,

        [XmlEnum("19")]
        [Description("辽宁中影北方")]
        Zhongyingbeifang = 19,

        [XmlEnum("20")]
        [Description("北京红鲤鱼院线")]
        Hongliyu = 20,

        [XmlEnum("21")]
        [Description("武汉天河院线")]
        Tianhe = 21,

        [XmlEnum("22")]
        [Description("浙江星光院线")]
        Xingguang = 22,

        [XmlEnum("23")]
        [Description("湖南潇湘院线")]
        Xiaoxiang = 23,

        [XmlEnum("24")]
        [Description("上海大光明院线")]
        Daguangming = 24,

        [XmlEnum("25")]
        [Description("福建中兴院线")]
        Zhongxing = 25,

        [XmlEnum("26")]
        [Description("山东新世纪")]
        Xingshiji = 26,

        [XmlEnum("27")]
        [Description("温州雁荡院线")]
        Yandang = 27,

        [XmlEnum("28")]
        [Description("世纪环球院线")]
        Huanqiu = 28,

        [XmlEnum("29")]
        [Description("长城沃美")]
        Womei = 29,

        [XmlEnum("30")]
        [Description("江苏东方院线")]
        Dongfang = 30,

        [XmlEnum("31")]
        [Description("九州中原")]
        Zhongyuan = 31,

        [XmlEnum("32")]
        [Description("河北中联院线")]
        Zhonglian = 32,

        [XmlEnum("33")]
        [Description("江西星河院线")]
        Xinghe = 33,

        [XmlEnum("34")]
        [Description("湖南楚湘院线")]
        Chuxiang = 34,

        [XmlEnum("35")]
        [Description("吉林吉影院线")]
        Jiying = 35,

        [XmlEnum("36")]
        [Description("鲁信院线")]
        Luxing = 36,

        [XmlEnum("37")]
        [Description("华夏新华大地院线")]
        Xinghuadadi = 37,

        [XmlEnum("38")]
        [Description("中广院线")]
        Zhongguang = 38,

        [XmlEnum("39")]
        [Description("四川峨眉院线")]
        Ermei = 39,

        [XmlEnum("40")]
        [Description("贵州星空院线")]
        Xingkong = 40,

        [XmlEnum("41")]
        [Description("新疆华夏天山院线")]
        Huaxiatianshan = 41,

        [XmlEnum("42")]
        [Description("内蒙古民族院线")]
        Mingzhou = 42,

        [XmlEnum("43")]
        [Description("西安长安院线")]
        Changan = 43,

        [XmlEnum("44")]
        [Description("北京明星时代院线")]
        Mingxingshida = 44,

        [XmlEnum("45")]
        [Description("上海弘歌院线")]
        Hongge = 45,

        [XmlEnum("46")]
        [Description("天津银光院线")]
        Yingguang = 46,

        [XmlEnum("47")]
        [Description("新疆电影公司")]
        Xingjiangdianying = 47,

        [XmlEnum("48")]
        [Description("华夏星火院线")]
        Xinghuo = 48
    }
}
