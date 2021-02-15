import java.util.ArrayList;

public class Processor {
    private final ResultDocs resultDocs;

    public Processor() {
        DocsFileReader docsFileReader = new DocsFileReader("C:\\Users\\hos3in\\Desktop\\Winter99-Co-op-Hossein-Aghaei\\phase02-code review\\EnglishData");
        this.resultDocs = new ResultDocs(new HashInvertedIndex(docsFileReader), docsFileReader);
    }

    private ArrayList<String> getFinalSet(ArrayList<String> noSignWords, ArrayList<String> plusWords) {
        ArrayList<String> finalSet = resultDocs.getInitialFinalSet(noSignWords, plusWords);
        finalSet.removeIf(doc -> !resultDocs.getMinusDocs().contains(doc));
        finalSet.removeIf(doc -> !resultDocs.getPlusDocs().contains(doc) && !resultDocs.getPlusDocs().isEmpty());
        return finalSet;
    }


    public ArrayList<String> search(ArrayList<ArrayList<String>> allWordsDocsToSearch) {
        setDocs(allWordsDocsToSearch);
        return getFinalSet(allWordsDocsToSearch.get(0), allWordsDocsToSearch.get(1));
    }


    private void setDocs(ArrayList<ArrayList<String>> allWordsDocsToSearch) {
        resultDocs.setNoSignDocs(allWordsDocsToSearch.get(0));
        resultDocs.setPlusDocs(allWordsDocsToSearch.get(1));
        resultDocs.setMinusDocs(allWordsDocsToSearch.get(2));
    }
}