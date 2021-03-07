using Model;
using search;

namespace main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var context = new SearchContextFactory().CreateDbContext(args);
            var docFileReader = new DocFileReader("EnglishData");
            var documentRepository = new DocumentRepository(context);
            var indexCreator = new IndexCreator(docFileReader, documentRepository);
            indexCreator.OrganizeDocsAndWords();
            var controller = new Controller(new ConsoleView(), new Processor(documentRepository));
            controller.Run();
        }
    }
}