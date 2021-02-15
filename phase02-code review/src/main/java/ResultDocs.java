import java.util.ArrayList;

public class ResultDocs implements Docs {
    private final HashInvertedIndex hashInvertedIndex;
    private final DocsFileReader docsFileReader;
    private ArrayList<String> noSignDocs = new ArrayList<>();
    private ArrayList<String> plusDocs = new ArrayList<>();
    private ArrayList<String> minusDocs = new ArrayList<>();

    public ResultDocs(HashInvertedIndex hashInvertedIndex, DocsFileReader docsFileReader) {
        this.hashInvertedIndex = hashInvertedIndex;
        this.docsFileReader = docsFileReader;
    }

    @Override
    public ArrayList<String> getNoSignDocs() {
        return noSignDocs;
    }

    @Override
    public void setNoSignDocs(ArrayList<String> noSignWords) {
        if (noSignDocs.isEmpty()){
            ArrayList<String> docs = checkFirstNoSignWord(noSignWords);
            for (int i = 1; i < noSignWords.size(); i++)
                if (hashInvertedIndex.contain(noSignWords.get(i))) {
                    ArrayList<String> docsOfWord = new ArrayList<>(hashInvertedIndex.getDocsContain(noSignWords.get(i)));
                    docs.removeIf(doc -> !docsOfWord.contains(doc));
                } else {
                    noSignDocs.clear();
                    return;
                }
            noSignDocs = docs;
        }
    }

    private ArrayList<String> checkFirstNoSignWord(final ArrayList<String> noSignWords) {
        ArrayList<String> docs = new ArrayList<>();
        if (!noSignWords.isEmpty())
            if (hashInvertedIndex.contain(noSignWords.get(0)))
                docs.addAll(hashInvertedIndex.getDocsContain(noSignWords.get(0)));
        return docs;
    }

    @Override
    public ArrayList<String> getPlusDocs() {
        return plusDocs;
    }

    @Override
    public void setPlusDocs(ArrayList<String> plusWords) {
        if (!plusDocs.isEmpty())
            return;
        ArrayList<String> docs = new ArrayList<>();
        for (final String word : plusWords)
            if (hashInvertedIndex.contain(word))
                docs.addAll(hashInvertedIndex.getDocsContain(word));
        plusDocs = docs;
    }

    @Override
    public ArrayList<String> getMinusDocs() {
        return minusDocs;
    }

    @Override
    public void setMinusDocs(ArrayList<String> minusWords) {
        if (minusDocs.isEmpty()) {
            ArrayList<String> docs = new ArrayList<>(docsFileReader.scanDocs().keySet());
            for (final String word : minusWords)
                if (hashInvertedIndex.contain(word))
                    docs.removeAll(hashInvertedIndex.getDocsContain(word));
            minusDocs = docs;
        }
    }
}