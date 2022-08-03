namespace Kurisu.Elasticsearch.Abstractions
{
    public interface IElasticsearchIndexGeneration
    {
        /// <summary>
        /// 返回默认的tmpIndex
        /// </summary>
        /// <param name="tmpIndex">typeof(T).Name</param>
        /// <returns></returns>
        string Generate(string tmpIndex);
    }
}