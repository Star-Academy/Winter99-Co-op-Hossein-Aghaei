import java.util.ArrayList;

public class Processor {
    private static Processor instance;
    private final FileReader fileReader;
    private final InvertedIndex invertedIndex;
    private final View view;
    private ArrayList<String> withoutSignDocs;
    private ArrayList<String> plusDocs;
    private ArrayList<String> minusDocs;

    public Processor() {
        fileReader = FileReader.getInstance();
        invertedIndex = InvertedIndex.getInstance();
        view = View.getInstance();
        withoutSignDocs = new ArrayList<>();
        plusDocs = new ArrayList<>();
        minusDocs = new ArrayList<>();
    }

    private ArrayList<String> getWithoutSignDocs() {
        ArrayList<String> docs = new ArrayList<>();
        if (!view.getWithoutSignWords().isEmpty())
            if (invertedIndex.allWordsContain(view.getWithoutSignWords().get(0)))
                docs.addAll(invertedIndex.getDocsContain(view.getWithoutSignWords().get(0)));
            else
                return docs;
        for (int i = 1; i < view.getWithoutSignWords().size(); i++)
            if (invertedIndex.allWordsContain(view.getWithoutSignWords().get(i))) {
                ArrayList<String> docsOfWord = new ArrayList<>(invertedIndex.getDocsContain(view.getWithoutSignWords().get(i)));
                docs.removeIf(doc -> !docsOfWord.contains(doc));
            } else {
                docs.clear();
                return docs;
            }
        return docs;
    }

    private ArrayList<String> getMinusDocs() {
        ArrayList<String> docs = new ArrayList<>(fileReader.getDocs().keySet());
        for (final String word : view.getMinusWords())
            if (invertedIndex.allWordsContain(word))
                docs.removeAll(invertedIndex.getDocsContain(word));
        return docs;
    }

    private ArrayList<String> getPlusDocs() {
        ArrayList<String> docs = new ArrayList<>();
        for (final String word : view.getPlusWords())
            if (invertedIndex.allWordsContain(word))
                docs.addAll(invertedIndex.getDocsContain(word));
        return docs;
    }

    public ArrayList<String> getFinalSet() {
        ArrayList<String> finalSet = new ArrayList<>(getInitialFinalSet());
        finalSet.removeIf(doc -> !minusDocs.contains(doc));
        finalSet.removeIf(doc -> !plusDocs.contains(doc) && !plusDocs.isEmpty());
        return finalSet;
    }

    private ArrayList<String> getInitialFinalSet() {
        withoutSignDocs = getWithoutSignDocs();
        plusDocs = getPlusDocs();
        minusDocs = getMinusDocs();
        ArrayList<String> initialFinalSet = new ArrayList<>();
        if (view.getWithoutSignWords().isEmpty())
            if (view.getPlusWords().isEmpty())
                initialFinalSet.addAll(minusDocs);
            else
                initialFinalSet.addAll(plusDocs);
        else
            initialFinalSet.addAll(withoutSignDocs);
        return initialFinalSet;
    }

    public static Processor getInstance() {
        if (instance == null)
            instance = new Processor();
        return instance;
    }
}
