using Elasticsearch.Net;

namespace Kurisu.Elasticsearch
{
    /// <summary>
    /// es 配置
    /// </summary>
    public class ElasticSearchSetting
    {
        /// <summary>
        /// 索引前缀
        /// </summary>
        public string DefaultIndexPrefix { get; set; }

        /// <summary>
        /// 连接配置
        /// </summary>
        public ConnectionConfiguration ConnectionConfiguration { get; set; }
    }
}