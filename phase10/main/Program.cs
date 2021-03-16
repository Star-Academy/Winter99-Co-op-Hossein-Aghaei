using elasticsearch;
using elasticsearch.model;
using Nest;

namespace main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            const string index = "inverted_index";
            const string documentsPath = "EnglishData";
            var client = ElasticClientFactory.CreateElasticClient();
            var queryCreator = new QueryCreator();
            var searcher = new Searcher(client, queryCreator, index);
            var consoleView = new ConsoleView();
            var controller = new Controller(consoleView, searcher);
            if (!IsIndexExisting(index, client))
            {
                var indexCreator = new IndexCreator(client);
                indexCreator.CreateIndex(index);
                var fileReader = new DocFileReader();
                var docCreator = new DocFactory(fileReader);
                var importer = new Importer<Doc>(client);
                importer.Import(docCreator.GetAllDocuments(documentsPath), index);
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

