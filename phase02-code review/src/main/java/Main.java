public class Main {
    public static void main(String[] args) {
        DocsFileReader docsFileReader = new DocsFileReader("C:\\Users\\hos3in\\Desktop\\Winter99-Co-op-Hossein-Aghaei\\phase02-code review\\EnglishData");
        IndexCreator indexCreator = new IndexCreator(docsFileReader);
        ResultDocs resultDocs = new ResultDocs(new HashInvertedIndex(indexCreator.organizeDocsAndWords()), docsFileReader);
        new Controller(new ConsoleView(), new Processor(resultDocs)).run();
    }
}