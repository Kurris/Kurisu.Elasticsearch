using Newtonsoft.Json;

namespace Kurisu.Elasticsearch.LowLinq.Syntax
{
    internal class EsSort
    {
        public EsSort(EsSortType esSortType)
        {
            Order = esSortType == EsSortType.Asc ? "asc" : "desc";
        }

        [JsonProperty("order")]
        public string Order { get; set; }
    }

    internal enum EsSortType
    {
        Asc = 0,
        Desc = 1,
    }
}