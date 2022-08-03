using System.Collections.Generic;

namespace Kurisu.Elasticsearch.LowLinq.Syntax
{
    internal class EsPage
    {
        internal Dictionary<string, int> Build(int pageIndex, int pageSize)
        {
            return new Dictionary<string, int>
            {
                ["from"] = (pageIndex - 1) * pageSize,
                ["size"] = pageSize
            };
        }
    }
}