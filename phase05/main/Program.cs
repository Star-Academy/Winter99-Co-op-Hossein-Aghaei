using search;

namespace main
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var docFileReader = new DocFileReader("EnglishData");
            var indexCreator = new IndexCreator(docFileReader);
            var hashInvertedIndex = new HashInvertedIndex(indexCreator.OrganizeDocsAndWords());
            var controller = new Controller(new ConsoleView(), new Processor(hashInvertedIndex));
            controller.Run();
        }
    }
}