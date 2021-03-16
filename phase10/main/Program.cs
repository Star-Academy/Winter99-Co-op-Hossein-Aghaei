using System;
using elasticsearch;

namespace main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var client = ElasticClientFactory.CreateElasticClient();
            var indexCreator = new IndexCreator(client);
            
        }
    }
}

