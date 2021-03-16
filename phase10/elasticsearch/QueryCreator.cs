using System.Collections.Generic;
using Nest;

namespace elasticsearch
{
    public class QueryCreator : IBoolQuery
    {
        public QueryContainer CreateBoolQuery(DocContainer input)
        {
            var boolQuery = new BoolQuery
            {
                Must = SetupMustQuery(input.NoSignWords),
                MustNot = SetupMustNotQuery(input.MinusSignWords),
            };
            if (string.IsNullOrWhiteSpace(input.PlusSignWords)) return boolQuery;

            boolQuery.Should = SetupShouldQuery(input.PlusSignWords, input.NoSignWords);
            boolQuery.MinimumShouldMatch = 1;

            return boolQuery;
        }

        private static IEnumerable<QueryContainer> SetupShouldQuery(string plusSignWords, string noSignWords)
        {
            var shouldQuery = new List<QueryContainer>
            {
                new MatchQuery
                {
                    Field = "content",
                    Query = plusSignWords.Length == 0 ? noSignWords : plusSignWords,
                    AutoGenerateSynonymsPhraseQuery = true,
                    Operator = Operator.Or,
                    Analyzer = Analyzer.CustomAnalyzer
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
                    Analyzer = Analyzer.CustomAnalyzer
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