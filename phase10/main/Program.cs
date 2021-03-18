using elasticsearch;
using elasticsearch.model;
using Nest;

namespace main
{
    internal static class Program
    {
        private const string Index = "inverted_index";
        private const string DocumentsPath = "EnglishData";
        private static void Main(string[] args)
        {
            var client = ElasticClientFactory.CreateElasticClient();
            var queryCreator = new QueryCreator();
            var searcher = new Searcher(client, queryCreator, Index);
            var consoleView = new ConsoleView();
            var controller = new Controller(consoleView, searcher);
            if (!IsIndexExisting(Index, client))
            {
                var indexCreator = new IndexCreator(client);
                indexCreator.CreateIndex(Index);
                var fileReader = new DocFileReader();
                var docCreator = new DocFactory(fileReader);
                var importer = new Importer<Doc>(client);
                importer.Import(docCreator.GetAllDocuments(DocumentsPath), Index);
            }
            controller.Run();
        }
        
        private static bool IsIndexExisting(string indexName, IElasticClient client)
        {
            var response = client.Indices.Exists(indexName);
            return response.Exists;
        }
    }
}

