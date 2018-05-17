using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WeiXinTicketSystem.Models
{
    public class DynatablePageModel
    {

        #region Member Varibale

        private Dictionary<string, string> queries;
        private Dictionary<string, int> sorts;
        private string sortKey;
        private string sortDirection;
        // private int type;

        #endregion

        #region Property

        public int Page { get; set; }
        public int PerPage { get; set; }
        public int Offset { get; set; }

        public Dictionary<string, string> Queries
        {
            get { return queries; }
            set
            {
                queries = value;
            }
        }
        public string Search { get; set; }

        public Dictionary<string, int> Sorts
        {
            get { return sorts; }
            set
            {
                sorts = value;

                if (sorts == null || sorts.Count == 0)
                {
                }
                else
                {
                    var sort = sorts.FirstOrDefault();
                    SortKey = sort.Key;
                    SortDirection = sort.Value > 0 ? "asc" : "desc";
                }

            }
        }

        public string SortKey
        {
            get
            {
                if (sortKey == null) return "updated";
                return sortKey;
            }
            set { sortKey = value; }
        }

        public string SortDirection
        {
            get
            {
                if (sortDirection == null) return "desc";
                return sortDirection;
            }
            set { sortDirection = value; }
        }

        #endregion


        public string GetSortKeyOrDefault(string key)
        {
            if (sortKey == null)
            {
                return key;
            }
            return sortKey;
        }

        public string GetSortDirectionOrDefault(string direction)
        {
            return sortDirection == null ? direction : sortDirection;
        }

    }

    public class DynatablePageModel<TQuery> where TQuery : DynatablePageQueryModel
    {
        public int PerPage { get; set; }
        public int Offset { get; set; }

        private TQuery _query;
        public TQuery Query
        {
            get
            {
                if (_query == null)
                {
                    _query = Activator.CreateInstance<TQuery>();
                }
                return _query;
            }
            set
            {
                _query = value;
            }
        }

        public IDictionary<string, string> Queries
        {
            set
            {
                if (value != null && value.Count > 0)
                {
                    Query = JsonConvert.DeserializeObject<TQuery>(JsonConvert.SerializeObject(value));
                }
            }
            get
            {
                return null;
            }
        }

        private DynatablePageSortModel _sort;
        public DynatablePageSortModel Sort
        {
            get
            {
                if (_sort == null)
                {
                    _sort = new DynatablePageSortModel() { Direction = "desc" };
                }
                return _sort;
            }
            set
            {
                _sort = value;
            }
        }

        public IDictionary<string, int> Sorts
        {
            set
            {
                if (value != null && value.Count > 0)
                {
                    Sort = new DynatablePageSortModel(value.First());
                }
            }
            get
            {
                return null;
            }
        }
    }

    public class DynatablePageQueryModel
    {
        public string Search { get; set; }
    }

    public class DynatablePageSortModel
    {
        public DynatablePageSortModel()
        { }

        public DynatablePageSortModel(KeyValuePair<string, int> pair)
        {
            Key = pair.Key;
            Direction = pair.Value > 0 ? "asc" : "desc";
        }

        public string Key { get; set; }
        public string Direction { get; set; }

        public string GetKeyOrDefault(string defaultKey)
        {
            return Key ?? defaultKey;
        }
    }
}