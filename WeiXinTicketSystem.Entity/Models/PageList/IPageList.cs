using System.Collections.Generic;

namespace WeiXinTicketSystem.Entity.Models.PageList
{
    /// <summary>
    /// 分页列表实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPageList<T> : IList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageStart { get; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 总数
        /// </summary>
        int TotalCount { get; }
    }
}
