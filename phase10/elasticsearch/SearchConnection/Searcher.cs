using System;
using System.Collections.Generic;
using System.Linq;
using elasticsearch.model;
using Nest;

namespace elasticsearch
{
    public class Searcher : ISearch
    {
        private readonly IElasticClient _client;
        private readonly IBoolQueryCreator _queryCreator;
        private readonly string _index;

        public Searcher(IElasticClient elasticsearch, IBoolQueryCreator queryCreator, string index)
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
            //response.Validate();
            Console.WriteLine();
            return response.Documents.Select(doc => doc.Name).ToHashSet();
        }

    }
}