using elasticsearch.model;
using Nest;

namespace elasticsearch
{
    public class IndexCreator 
    {
        private readonly IElasticClient _client;
      
        public IndexCreator(IElasticClient client)
        {
            _client = client;
        }

        public void CreateIndex(string indexName)
        {
            var response = _client.Indices.Create(indexName,
                descriptor => descriptor.Settings(SetupSetting).
                    Map<Doc>(SetupMapping));
            response.Validate();

        }

        private static ITypeMapping SetupMapping(TypeMappingDescriptor<Doc> typeMapping)
        {
            return typeMapping.Properties(SetupProperties);
        }

        private static IPromise<IProperties> SetupProperties(PropertiesDescriptor<Doc> properties)
        {
            return properties.Keyword(k => k.
                Name(doc => doc.Name)).
                Text(t => t.
                    Name(doc => doc.Content).
                    Analyzer(Analyzer.CustomAnalyzer));
        }

        private static IPromise<IIndexSettings> SetupSetting(IndexSettingsDescriptor indexSettings)
        {
            return indexSettings.
                Setting("max_ngram_diff", 7).
                Analysis(SetupAnalysis);
        }

        private static IAnalysis SetupAnalysis(AnalysisDescriptor analysis)
        {
            return analysis.
                TokenFilters(SetupTokenFilter).
                Analyzers(SetupAnalyzer);
        }

        private static IPromise<IAnalyzers> SetupAnalyzer(AnalyzersDescriptor analyzersDescriptor)
        {
            return analyzersDescriptor.Custom(Analyzer.CustomAnalyzer,
                s => s.Tokenizer(TokenFilter.Standard).
                    Filters(TokenFilter.LowerCase, 
                        TokenFilter.WordDelimiter, 
                        TokenFilter.EnglishStopWords,
                        TokenFilter.NGram));
        }

        private static IPromise<ITokenFilters> SetupTokenFilter(TokenFiltersDescriptor tokenFilters)
        {
            return tokenFilters.
                NGram(TokenFilter.NGram, s => s.
                    MinGram(3).
                    MaxGram(10));
        }
    }
} 