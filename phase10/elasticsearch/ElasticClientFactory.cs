using System;
using Nest;

namespace elasticsearch
{
    public static class ElasticClientFactory
    {
        private static readonly IElasticClient ElasticClient;

        static ElasticClientFactory()
        {
            ElasticClient = CreateClient();
        }

        private static IElasticClient CreateClient()
        {
            var uri = new Uri("http://localhost:9200");
            var connectionString = new ConnectionSettings(uri);
            return new ElasticClient(connectionString);
        }

        public static IElasticClient CreateElasticClient()
        {
            return ElasticClient;
        }
    }
}