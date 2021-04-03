using System.Collections.Generic;
using elasticsearch.DocManager;
using Elasticsearch.Net;
using elasticsearch.SearchConnection;
using Nest;
using Xunit;

namespace elasticsearchTest
{
    public class QueryCreatorTest
    {
        private readonly IBoolQueryCreator _sut;

        public QueryCreatorTest()
        {
            _sut = new QueryCreator();
        }


        [Fact]
        public void CreateBoolQuery_ShouldCreateAppropriateQuery_WhenEveryThingIsOk()
        {
            var expectedQuery = CreateExpectedQuery();

            var docContainer = new DocContainer()
                {NoSignWords = "hello", PlusSignWords = "help mom", MinusSignWords = "dad street"};
            var actualQuery = _sut.CreateBoolQuery(docContainer);

            var serializer = new ElasticClient().RequestResponseSerializer;
            
            Assert.Equal(serializer.SerializeToString(expectedQuery), serializer.SerializeToString(actualQuery));
        }

        private static QueryContainer CreateExpectedQuery()
        {
            QueryContainer expectedQuery = new BoolQuery
            {
                Must = new List<QueryContainer>()
                {
                    new MatchQuery
                    {
                        Field = "content",
                        AutoGenerateSynonymsPhraseQuery = true,
                        Query = "hello",
                        Operator = Operator.And
                    }
                },
                MustNot = new List<QueryContainer>()
                {
                    new MatchQuery
                    {
                        Field = "content",
                        Query = "dad street",
                        AutoGenerateSynonymsPhraseQuery = true,
                        Operator = Operator.Or,
                    }
                },
                Should = new List<QueryContainer>
                {
                    new MatchQuery
                    {
                        Field = "content",
                        Query = "help mom",
                        AutoGenerateSynonymsPhraseQuery = true,
                        Operator = Operator.Or,
                    }
                },
                MinimumShouldMatch = 1
            };
            return expectedQuery;
        }
    }
}