using Microsoft.Extensions.Configuration;
using elasticsearch;
using elasticsearch.DocManager;
using elasticsearch.model;
using elasticsearch.SearchConnection;
using Nest;

namespace main
{
    internal static class Program
    {
        private const string Index = "inverted_index";
        private static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var directoryPath = config.GetValue<string>("DirectoryPath");
            
            var client = new ElasticClientFactory(config).CreateElasticClient();
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
                importer.Import(docCreator.GetAllDocuments(directoryPath), Index);
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

