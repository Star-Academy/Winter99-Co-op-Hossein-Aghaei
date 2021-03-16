using System.Collections.Generic;
using System.Linq;
using elasticsearch.model;
using Nest;

namespace elasticsearch
{
    public class Searcher : ISearch
    {
        private readonly IElasticClient _client;
        private readonly IBoolQuery _queryCreator;
        private readonly string _index;

        public Searcher(IElasticClient elasticsearch, IBoolQuery queryCreator, string index)
        {
            _client = elasticsearch;
            _index = index;
            _queryCreator = queryCreator;
        }

        public HashSet<string> Search(DocContainer input)
        {
            var query = _queryCreator.CreateBoolQuery(input);
            
            var response = _client.Search<Doc>(s => s.
                Index(_index).
                Query(q => query).
                Size(1000));
            
            return response.Documents.Select(doc => doc.Name).ToHashSet();
        }

    }
}