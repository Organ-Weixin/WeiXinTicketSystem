using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;

namespace WeiXinTicketSystem.Models
{
    public class DynatableResultModel
    {
        public IEnumerable<dynamic> records { get; set; }

        public int queryRecordCount { get; set; }
        public int totalRecordCount { get; set; }


        public DynatableResultModel()
        {
        }

        public DynatableResultModel(IEnumerable<dynamic> records, int total, int query)
        {
            this.records = records;
            this.queryRecordCount = query;
            this.totalRecordCount = total;
        }

        public DynatableResultModel(IEnumerable<dynamic> records, int totalRecordCount)
        {
            this.records = records;
            this.queryRecordCount = totalRecordCount;
            this.totalRecordCount = totalRecordCount;
        }
    }

    public static class DynatableResultModelExtensions
    {
        public static DynatableResultModel ToDynatableResultModel<T>(this IList<T> records, int perPage, int offset, Func<T, int, object> mapping)
        {
            return new DynatableResultModel(records.Skip(offset).Take(perPage).Select((t, i) => mapping(t, i)), records.Count);
        }

        public static DynatableResultModel ToDynatableModel<T>(this IList<T> records, int total, int offset, Func<T, object> mapping)
        {
            return new DynatableResultModel(records.Select((t, i) =>
            {
                var item = mapping(t).ToDynamic();
                item.row = i + offset + 1;
                return item;
            }), total);
        }

        public static DynatableResultModel ToDynatableModel<T>(this IList<T> records, int total, int offset, Func<T, int, object> mapping)
        {
            return new DynatableResultModel(records.Select((t, i) =>
            {
                var item = mapping(t, i + offset).ToDynamic();
                item.row = i + offset + 1;
                return item;
            }), total);
        }

        public static dynamic ToDynamic(this object value)
        {
            IDictionary<string, object> expando = new ExpandoObject();

            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value.GetType()))
                expando.Add(property.Name, property.GetValue(value));

            return expando as ExpandoObject;
        }
    }
}