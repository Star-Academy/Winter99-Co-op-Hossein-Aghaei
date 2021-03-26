using System.Collections.Generic;
using Elasticsearch.Net;
using Nest;

namespace elasticsearch
{
    public class Importer<T> where T : class
    {
        private readonly IElasticClient _client;

        public Importer(IElasticClient client)
        {
            _client = client;
        }

        public void Import(IEnumerable<T> documents, string indexName)
        {
            var bulk = SetupBulk(documents, indexName);
            var response = _client.Bulk(bulk);
            response.Validate();
        }

        private static BulkDescriptor SetupBulk(IEnumerable<T> documents, string indexName)
        {
            var bulk = new BulkDescriptor();
            foreach (var doc in documents)
            {
                bulk.Index<T>(i => 
                    i.Index(indexName).
                        Document(doc));
            }
            return bulk;
        }
        
    }
}