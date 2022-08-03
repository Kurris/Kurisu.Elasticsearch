using System.Collections.Generic;
using Kurisu.Elasticsearch.Abstractions;

namespace Kurisu.Elasticsearch.LowLinq.Syntax
{
    /// <summary>
    /// post item
    /// </summary>
    internal class EsItem : BaseElasticsearchItem
    {
        public EsItem(Dictionary<string, object> properties) : base(properties)
        {
        }
    }
}