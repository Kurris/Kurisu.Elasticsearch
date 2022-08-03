using Elasticsearch.Net;

namespace Kurisu.Elasticsearch.Abstractions
{
    /// <summary>
    /// elastic search service
    /// </summary>
    public interface IElasticSearchService
    {
        /// <summary>
        /// linq search client
        /// </summary>
        public IElasticLowLevelClient LowLevelClient { get; set; }

        /// <summary>
        /// es index
        /// </summary>
        public string DefaultIndexPrefix { get; }


        /// <summary>
        /// POST DSL 查询
        /// </summary>
        /// <param name="postData"></param>
        /// <typeparam name="TDocument"></typeparam>
        /// <returns></returns>
        ISearchable<TDocument> PostSearch<TDocument>(PostData postData = null) where TDocument : class, new();
    }
}