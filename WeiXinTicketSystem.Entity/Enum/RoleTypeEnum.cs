using System.ComponentModel;

namespace WeiXinTicketSystem.Entity.Enum
{
    public enum RoleTypeEnum : byte
    {
        [Description("系统管理员")]
        SystemAdmin = 1,

        [Description("影院管理员")]
        CinemaAdmin = 2,

        //前两种为系统预置，系统管理员角色不可修改、删除，影院管理员角色不可删除

        [Description("自定义")]
        Customized = 3
    }
}
