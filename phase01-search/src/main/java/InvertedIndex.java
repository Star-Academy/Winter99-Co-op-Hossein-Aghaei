import opennlp.tools.stemmer.PorterStemmer;

import java.util.ArrayList;
import java.util.HashMap;

public class InvertedIndex {
    private static InvertedIndex instance;
    private final HashMap<String, ArrayList<String>> words;
    private final PorterStemmer porterStemmer;

    public InvertedIndex() {
        words = new HashMap<>();
        porterStemmer = new PorterStemmer();
    }

    public void organize() {
        HashMap<String, String> docs = FileReader.getInstance().getDocs();
        for (String doc : docs.keySet()) {
            query(doc, docs.get(doc));
        }
    }

    private void query(final String docName, final String content) {
        String[] split = content.split("[\\W]+");
        for (final String word : split) {
            String stemWord = porterStemmer.stem(word);
            if (words.containsKey(stemWord)) {
                ArrayList<String> container = words.get(stemWord);
                if (!container.contains(docName))
                    container.add(docName);
            } else {
                ArrayList<String> container = new ArrayList<String>();
                container.add(docName);
                words.put(stemWord, container);
            }
        }
    }

    public HashMap<String, ArrayList<String>> getWords() {
        return words;
    }

    public static InvertedIndex getInstance() {
        if(instance == null)
            instance = new InvertedIndex();
        return instance;
    }
}