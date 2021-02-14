import java.util.ArrayList;

public class ResultDocs extends Docs implements DocOperation {
    private static ResultDocs instance;
    private final HashInvertedIndex hashInvertedIndex = HashInvertedIndex.getInstance();
    private final InputDocs inputDocs = InputDocs.getInstance();
    private final Object noSignLock = new Object();
    private final Object plusSignLock = new Object();
    private final Object minusSignLock = new Object();


    @Override
    public ArrayList<String> getFinalNoSignDocs(final ArrayList<String> noSignWords) {
        ArrayList<String> docs = new ArrayList<>();
        if (!noSignWords.isEmpty())
            if (hashInvertedIndex.allWordsContain(noSignWords.get(0)))
                docs.addAll(hashInvertedIndex.getDocsContain(noSignWords.get(0)));
            else
                return docs;
        for (int i = 1; i < noSignWords.size(); i++)
            if (hashInvertedIndex.allWordsContain(noSignWords.get(i))) {
                ArrayList<String> docsOfWord = new ArrayList<>(hashInvertedIndex.getDocsContain(noSignWords.get(i)));
                docs.removeIf(doc -> !docsOfWord.contains(doc));
            } else {
                docs.clear();
                return docs;
            }
        return docs;
    }

    @Override
    public ArrayList<String> getFinalPlusDocs(ArrayList<String> plusSignWords) {
        ArrayList<String> docs = new ArrayList<>();
        for (final String word : plusSignWords)
            if (hashInvertedIndex.allWordsContain(word))
                docs.addAll(hashInvertedIndex.getDocsContain(word));
        return docs;
    }

    @Override
    public ArrayList<String> getFinalMinusDocs(ArrayList<String> minusSignWords) {
        ArrayList<String> docs = new ArrayList<>(inputDocs.getAllDocs(inputDocs.getDocsDirectoryFile().listFiles()).keySet());
        for (final String word : minusSignWords)
            if (hashInvertedIndex.allWordsContain(word))
                docs.removeAll(hashInvertedIndex.getDocsContain(word));
        return docs;
    }

    @Override
    public ArrayList<String> getNoSignDocs() {
        if (noSignDocs.isEmpty())
            setNoSignDocs(inputDocs.getNoSignDocs());
        return noSignDocs;
    }

    @Override
    public void setNoSignDocs(ArrayList<String> noSignWords) {
        if (noSignDocs.isEmpty()) {
            synchronized (noSignLock) {
                if (noSignDocs.isEmpty())
                    noSignDocs = getFinalNoSignDocs(inputDocs.getNoSignDocs());
            }
        }
    }

    @Override
    public ArrayList<String> getPlusDocs() {
        if (plusDocs.isEmpty())
            setPlusDocs(inputDocs.getPlusDocs());
        return plusDocs;
    }

    @Override
    public void setPlusDocs(ArrayList<String> plusWords) {
        if (plusDocs.isEmpty()) {
            synchronized (plusSignLock) {
                if (plusDocs.isEmpty())
                    plusDocs = getFinalPlusDocs(inputDocs.getPlusDocs());
            }
        }
    }

    @Override
    public ArrayList<String> getMinusDocs() {
        if (minusDocs.isEmpty())
            setMinusDocs(inputDocs.getMinusDocs());
        return minusDocs;
    }

    @Override
    public void setMinusDocs(ArrayList<String> minusWords) {
        if (minusDocs.isEmpty()) {
            synchronized (minusSignLock) {
                if (minusDocs.isEmpty())
                    minusDocs = getFinalMinusDocs(inputDocs.getMinusDocs());
            }
        }
    }

    public ArrayList<String> getInitialFinalSet() {
        hashInvertedIndex.organizeDocsAndWords();
        ArrayList<String> initialFinalSet = new ArrayList<>();
        if (inputDocs.getNoSignDocs().isEmpty())
            if (inputDocs.getPlusDocs().isEmpty())
                initialFinalSet.addAll(getMinusDocs());
            else
                initialFinalSet.addAll(getPlusDocs());
        else
            initialFinalSet.addAll(getNoSignDocs());
        return initialFinalSet;
    }

    public static ResultDocs getInstance() {
        if (instance == null)
            instance = new ResultDocs();
        return instance;
    }
}