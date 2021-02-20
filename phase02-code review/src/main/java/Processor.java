import java.util.ArrayList;

public class Processor {
    private final ResultDocs resultDocs;

    public Processor(ResultDocs resultDocs) {
        this.resultDocs = resultDocs;
    }

    private ArrayList<String> getFinalSet(ArrayList<String> noSignWords, ArrayList<String> plusWords) {
        ArrayList<String> finalSet = getInitialFinalSet(noSignWords, plusWords);
        finalSet.removeIf(doc -> !resultDocs.getMinusDocs().contains(doc));
        finalSet.removeIf(doc -> !resultDocs.getPlusDocs().contains(doc) && !resultDocs.getPlusDocs().isEmpty());
        return finalSet;
    }

    private ArrayList<String> getInitialFinalSet(ArrayList<String> noSignWords, ArrayList<String> plusWords) {
        ArrayList<String> initialFinalSet = new ArrayList<>();
        if (!noSignWords.isEmpty()) {
            initialFinalSet.addAll(resultDocs.getNoSignDocs());
            return initialFinalSet;
        }
        if (!plusWords.isEmpty()) {
            initialFinalSet.addAll(resultDocs.getPlusDocs());
            return initialFinalSet;
        }
        initialFinalSet.addAll(resultDocs.getMinusDocs());
        return initialFinalSet;
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