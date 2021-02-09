import java.util.ArrayList;

public class Processor {
    private static Processor instance;
    private final FileReader fileReader;
    private final HashInvertedIndex hashInvertedIndex;
    private final ConsoleView consoleView;
    private ArrayList<String> withoutSignDocs;
    private ArrayList<String> plusDocs;
    private ArrayList<String> minusDocs;

    public Processor() {
        fileReader = FileReader.getInstance();
        hashInvertedIndex = HashInvertedIndex.getInstance();
        consoleView = ConsoleView.getInstance();
        withoutSignDocs = new ArrayList<>();
        plusDocs = new ArrayList<>();
        minusDocs = new ArrayList<>();
    }

    private ArrayList<String> getWithoutSignDocs() {
        ArrayList<String> docs = new ArrayList<>();
        if (!consoleView.getWithoutSignWords().isEmpty())
            if (hashInvertedIndex.allWordsContain(consoleView.getWithoutSignWords().get(0)))
                docs.addAll(hashInvertedIndex.getDocsContain(consoleView.getWithoutSignWords().get(0)));
            else
                return docs;
        for (int i = 1; i < consoleView.getWithoutSignWords().size(); i++)
            if (hashInvertedIndex.allWordsContain(consoleView.getWithoutSignWords().get(i))) {
                ArrayList<String> docsOfWord = new ArrayList<>(hashInvertedIndex.getDocsContain(consoleView.getWithoutSignWords().get(i)));
                docs.removeIf(doc -> !docsOfWord.contains(doc));
            } else {
                docs.clear();
                return docs;
            }
        return docs;
    }

    private ArrayList<String> getMinusDocs() {
        ArrayList<String> docs = new ArrayList<>(fileReader.getDocs().keySet());
        for (final String word : consoleView.getMinusWords())
            if (hashInvertedIndex.allWordsContain(word))
                docs.removeAll(hashInvertedIndex.getDocsContain(word));
        return docs;
    }

    private ArrayList<String> getPlusDocs() {
        ArrayList<String> docs = new ArrayList<>();
        for (final String word : consoleView.getPlusWords())
            if (hashInvertedIndex.allWordsContain(word))
                docs.addAll(hashInvertedIndex.getDocsContain(word));
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
        if (consoleView.getWithoutSignWords().isEmpty())
            if (consoleView.getPlusWords().isEmpty())
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