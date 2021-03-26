using System;
using System.Collections.Generic;
using elasticsearch;
using elasticsearch.model;
using Nest;
using NSubstitute;
using Xunit;

namespace elasticsearchTest
{
    public class SearcherTest
    {
        private readonly ISearch _sut;
        private readonly IElasticClient _client;

        public SearcherTest()
        {
            var queryCreator = Substitute.For<IBoolQueryCreator>();
            _client = Substitute.For<IElasticClient>();
            _sut = new Searcher(_client, queryCreator, "inverted_index");
        }

        [Fact]
        public void Search_ShouldReturnAppropriateResult_whenEveryThingIsOk()
        {
            var docContainer = new DocContainer() 
                {NoSignWords = "hello",
                    PlusSignWords = "dad",
                    MinusSignWords = "war"};
            var searchResponse = Substitute.For<ISearchResponse<Doc>>();
            searchResponse.Documents.Returns(new List<Doc>() {new Doc() {Name = "123", Content = "hello"}});

            _client.Search<Doc>(Arg.Any<Func<SearchDescriptor<Doc>, ISearchRequest>>()).Returns(searchResponse);

            Assert.Equal(new HashSet<string>() {"123"}, _sut.Search(docContainer));
        }
        
    }
}