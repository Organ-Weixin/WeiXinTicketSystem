using System.Collections.Generic;
using System.Linq;

namespace WeiXinTicketSystem.Entity.Models.PageList.Impl
{
    public class PageList<T> : List<T>, IPageList<T>
    {
        public PageList(IEnumerable<T> source, int pageStart, int pageSize, int totalCount)
        {
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.PageStart = pageStart;
            this.AddRange(source);
        }

        public PageList(IEnumerable<T> source)
        {
            this.TotalCount = source.Count();
            this.PageSize = this.TotalCount;
            this.PageStart = 0;
            this.AddRange(source);
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageStart { get; private set; }

        /// <summary>
        /// 页面尺寸
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; private set; }
    }

}
