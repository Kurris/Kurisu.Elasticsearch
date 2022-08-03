using System;
using Kurisu.Elasticsearch.Abstractions;
using Kurisu.Elasticsearch.Implements;
using Microsoft.Extensions.DependencyInjection;

namespace Kurisu.Elasticsearch.Extensions
{
    /// <summary>
    /// es post dls 服务扩展
    /// </summary>
    public static class ElasticSearchServiceCollectionExtensions
    {
        /// <summary>
        /// 注入post dls 查询服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddKurisuElasticsearch(this IServiceCollection services, Action<ElasticSearchSetting> options)
        {
            services.AddSingleton(typeof(IElasticSearchService), _ => new ElasticSearchService(options));
            return services;
        }

        /// <summary>
        /// 注入post dls 查询服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <typeparam name="TIndexGeneration"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddKurisuElasticsearch<TIndexGeneration>(this IServiceCollection services, Action<ElasticSearchSetting> options) where TIndexGeneration : IElasticsearchIndexGeneration, new()
        {
            services.AddSingleton(typeof(IElasticsearchIndexGeneration), typeof(TIndexGeneration));
            services.AddSingleton(typeof(IElasticSearchService), _ => new ElasticSearchService(options));
            return services;
        }
    }
}