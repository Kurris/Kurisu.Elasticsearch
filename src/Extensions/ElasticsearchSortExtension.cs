using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.LowLinq;
using Kurisu.Elasticsearch.LowLinq.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kurisu.Elasticsearch.Extensions
{
    public static class ElasticsearchSortExtension
    {
        public static ISearchable<TDocument> Where<TDocument>(this IOrderedList<TDocument> orderedList, Expression<Func<TDocument, bool>> filterExpression)
            where TDocument : class, new()
        {
            var sortable = orderedList.Searchable as IEsSortable;
            var dic = sortable.Orders ?? new Dictionary<string, object>();

            //替换
            var key = "sort";
            if (dic.ContainsKey(key))
            {
                dic.Remove(key);
            }

            dic.Add("sort", orderedList);

            orderedList.Searchable.GetType().GetProperty(nameof(IEsSortable.Orders)).SetValue(orderedList.Searchable, dic);
            return orderedList.Searchable.Where(filterExpression);
        }
    }
}