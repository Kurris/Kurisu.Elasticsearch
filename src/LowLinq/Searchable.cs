using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.Extensions;
using Nest;
using Newtonsoft.Json;

namespace Kurisu.Elasticsearch.LowLinq
{
    internal class Searchable<TDocument> : ISearchable<TDocument>, IEsSortable where TDocument : class, new()
    {
        private readonly IElasticLowLevelClient _client;
        private readonly string _index;
        private readonly PostData _postData;
        private Expression<Func<TDocument, bool>> _searchExpression;
        private readonly Dictionary<string, object> _properties = new();

        // private readonly Dictionary<string, int> _page = new();

        public IDictionary<string, object> Orders { get; set; }

        internal Searchable(IElasticSearchService elasticSearchService, string index = null, PostData postData = null)
        {
            _client = elasticSearchService.LowLevelClient;
            _index = index.ToLower();
            _postData = postData;
        }

        public async Task<List<TDocument>> ToListAsync()
        {
            var res = await BuildAsync();
            return res.ToList();
        }

        public ISearchable<TDocument> Where(Expression<Func<TDocument, bool>> filterExpression)
        {
            _searchExpression = _searchExpression == null ? filterExpression : _searchExpression.And(filterExpression);
            return this;
        }


        // public Task<Pagination<TDocument>> ToPagination(PageIn pageIn)
        // {
        //     _page = new EsPage().Build(pageIn);
        // }


        /// <summary>
        /// 构建Elasticsearch DSL并且获取Documents
        /// </summary>
        /// <returns></returns>
        private async Task<IReadOnlyCollection<TDocument>> BuildAsync()
        {
            if (_postData != null)
            {
                return (await _client.SearchAsync<SearchResponse<TDocument>>(_index, _postData)).Documents;
            }

            //条件过滤
            if (_searchExpression != null)
            {
                var translate = new ElasticsearchTranslate();
                var query = translate.GetQuery(_searchExpression);
                _properties.Add("query", query);
            }

            //排序
            if (Orders?.Count > 0)
            {
                var order = Orders.First();
                _properties.Add(order.Key, new List<object> {order.Value});
            }

            // //分页
            // if (_page?.Count > 0)
            // {
            //     foreach (var (key, value) in _page)
            //     {
            //         _properties.Add(key, value);
            //     }
            // }

            var searchBody = JsonConvert.SerializeObject(_properties);

#if DEBUG
            Console.WriteLine($@" 
GET {_index}/_search
{ConvertJsonString(searchBody)}");

#endif
            var response = await _client.SearchAsync<SearchResponse<TDocument>>(_index, PostData.String(searchBody));
            return response.Documents;
        }

#if DEBUG

        /// <summary>
        /// 格式化json
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertJsonString(string str)
        {
            //格式化json字符串
            var serializer = new JsonSerializer();
            using (TextReader tr = new StringReader(str))
            {
                using (JsonTextReader jtr = new JsonTextReader(tr))
                {
                    object obj = serializer.Deserialize(jtr);
                    if (obj != null)
                    {
                        using (StringWriter textWriter = new StringWriter())
                        {
                            using (JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                                   {
                                       Formatting = Formatting.Indented,
                                       Indentation = 4,
                                       IndentChar = ' '
                                   })
                            {
                                serializer.Serialize(jsonWriter, obj);
                                return textWriter.ToString();
                            }
                        }
                    }

                    return str;
                }
            }
        }

#endif
    }
}