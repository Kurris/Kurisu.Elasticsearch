using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.LowLinq.Syntax;

namespace Kurisu.Elasticsearch.LowLinq
{
    internal class OrderedList<TDocument> : EsItem, IOrderedList<TDocument> where TDocument : class, new()
    {
        private readonly ElasticsearchTranslate _translate = new();
        private ISearchable<TDocument> _searchable;

        public OrderedList(Dictionary<string, object> properties) : base(properties)
        {
        }

        public ISearchable<TDocument> Searchable => _searchable;


        internal IOrderedList<TDocument> OrderBy<TKey>(ISearchable<TDocument> searchable, Expression<Func<TDocument, TKey>> orderExpression)
        {
            _searchable = searchable;

            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Asc)); });

            return this;
        }


        internal IOrderedList<TDocument> OrderByDescending<TKey>(ISearchable<TDocument> searchable, Expression<Func<TDocument, TKey>> orderExpression)
        {
            _searchable = searchable;

            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Desc)); });

            return this;
        }


        public IOrderedList<TDocument> OrderBy<TKey>(Expression<Func<TDocument, TKey>> orderExpression)
        {
            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Asc)); });

            return this;
        }

        public IOrderedList<TDocument> OrderByDescending<TKey>(Expression<Func<TDocument, TKey>> orderExpression)
        {
            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Desc)); });

            return this;
        }


        public IOrderedList<TDocument> ThenBy<TKey>(Expression<Func<TDocument, TKey>> orderExpression)
        {
            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Asc)); });

            return this;
        }

        public IOrderedList<TDocument> ThenByDescending<TKey>(Expression<Func<TDocument, TKey>> orderExpression)
        {
            var orderFields = _translate.GetOrders(orderExpression);
            orderFields.ForEach(x => { Properties.TryAdd(x, new EsSort(EsSortType.Desc)); });

            return this;
        }
    }
}