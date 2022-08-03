using System;
using Elasticsearch.Net;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.LowLinq;

namespace Kurisu.Elasticsearch.Implements
{
    /// <summary>
    /// es查询服务
    /// </summary>
    public class ElasticSearchService : IElasticSearchService
    {
        public ElasticSearchService(Action<ElasticSearchSetting> options)
        {
            var esSetting = new ElasticSearchSetting();
            options.Invoke(esSetting);

            // var uris = new[]
            // {
            //     new Uri("http://192.168.1.4:31540/"),
            // };
            //
            // var nodes = new StaticConnectionPool(uris);
            //
            // var settings = new ConnectionConfiguration(nodes)
            //     .RequestTimeout(TimeSpan.FromSeconds(30))
            //     .BasicAuthentication("elastic", "zxc111");

            DefaultIndexPrefix = esSetting.DefaultIndexPrefix;
            LowLevelClient = new ElasticLowLevelClient(esSetting.ConnectionConfiguration);
        }


        public IElasticLowLevelClient LowLevelClient { get; set; }

        public string DefaultIndexPrefix { get; }


        /// <summary>
        /// Post 请求查询
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public ISearchable<TDocument> PostSearch<TDocument>(PostData postData = null) where TDocument : class, new()
        {
            var prefix = string.IsNullOrEmpty(DefaultIndexPrefix) ? string.Empty : DefaultIndexPrefix + ".";
            var index = prefix + typeof(TDocument).Name;
            return new Searchable<TDocument>(this, index.ToLowerInvariant(), postData);
        }
    }
}