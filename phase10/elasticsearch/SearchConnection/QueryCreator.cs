using System.Collections.Generic;
using elasticsearch.DocManager;
using Nest;

namespace elasticsearch.SearchConnection
{
    public class QueryCreator : IBoolQueryCreator
    {
        public QueryContainer CreateBoolQuery(DocContainer input)
        {
            var boolQuery = new BoolQuery
            {
                Must = SetupMustQuery(input.NoSignWords),
                MustNot = SetupMustNotQuery(input.MinusSignWords),
            };
            if (string.IsNullOrWhiteSpace(input.PlusSignWords)) return boolQuery;

            boolQuery.Should = SetupShouldQuery(input.PlusSignWords);
            boolQuery.MinimumShouldMatch = 1;

            return boolQuery;
        }

        private static IEnumerable<QueryContainer> SetupShouldQuery(string plusSignWords)
        {
            var shouldQuery = new List<QueryContainer>
            {
                new MatchQuery
                {
                    Field = "content",
                    Query = plusSignWords,
                    AutoGenerateSynonymsPhraseQuery = true,
                    Operator = Operator.Or,
                }
            };
            return shouldQuery;
        }

        private static IEnumerable<QueryContainer> SetupMustNotQuery(string minusSignWords)
        {
            var mustNotQuery = new List<QueryContainer>
            {
                new MatchQuery
                {
                    Field = "content",
                    Query = minusSignWords,
                    AutoGenerateSynonymsPhraseQuery = true,
                    Operator = Operator.Or,
                }
            };
            return mustNotQuery;
        }

        private static IEnumerable<QueryContainer> SetupMustQuery(string noSignWords)
        {
            var mustQuery = new List<QueryContainer>
            {
                new MatchQuery
                {
                    Field = "content",
                    AutoGenerateSynonymsPhraseQuery = true,
                    Query = noSignWords,
                    Operator = Operator.And
                }
            };
            return mustQuery;
        }
    }
}