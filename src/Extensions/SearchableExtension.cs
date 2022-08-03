using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.LowLinq;

namespace Kurisu.Elasticsearch.Extensions
{
    public static class SearchableExtension
    {
        // /// <summary>
        // /// Post 请求查询
        // /// </summary>
        // /// <param name="elasticSearchService"></param>
        // /// <param name="postData"></param>
        // /// <returns></returns>
        // public static ISearchable<TDocument> PostSearch<TDocument>(this IElasticSearchService elasticSearchService, PostData postData = null)
        //     where TDocument : class, new()
        // {
        //     var index = string.IsNullOrEmpty(elasticSearchService.Index) ? typeof(TDocument).Name : elasticSearchService.Index;
        //
        //     return new Searchable<TDocument>(elasticSearchService, index, postData);
        // }


        public static IOrderedList<TDocument> OrderBy<TDocument, TKey>(this ISearchable<TDocument> searchable, Expression<Func<TDocument, TKey>> orderExpression)
            where TDocument : class, new()
        {
            return new OrderedList<TDocument>(new Dictionary<string, object>()).OrderBy(searchable, orderExpression);
        }

        public static IOrderedList<TDocument> OrderByDescending<TDocument, TKey>(this ISearchable<TDocument> searchable, Expression<Func<TDocument, TKey>> orderExpression)
            where TDocument : class, new()
        {
            return new OrderedList<TDocument>(new Dictionary<string, object>()).OrderByDescending(searchable, orderExpression);
        }


        public static ISelectable<TDocument, TResult> Select<TDocument, TResult>(this ISearchable<TDocument> searchable, Expression<Func<TDocument, TResult>> selectExpression)
            where TDocument : class, new() where TResult : class
        {
            return new Selectable<TDocument, TResult>();
        }
    }
}