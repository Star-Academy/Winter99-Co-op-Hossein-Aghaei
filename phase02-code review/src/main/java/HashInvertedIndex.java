import opennlp.tools.stemmer.PorterStemmer;
import java.util.ArrayList;
import java.util.HashMap;

public class HashInvertedIndex implements InvertedIndex{
    private static HashInvertedIndex instance;
    private final HashMap<String, ArrayList<String>> words;
    private final PorterStemmer porterStemmer;
    private final FileReader fileReader;

    public HashInvertedIndex() {
        words = new HashMap<>();
        porterStemmer = new PorterStemmer();
        fileReader = FileReader.getInstance();
        organize();
    }

    public void organize() {
        HashMap<String, String> docs = fileReader.getDocs();
        for (final String doc : docs.keySet()) {
            query(doc, docs.get(doc));
        }
    }

    private void query(final String docName, final String content) {
        String[] splitContent = content.split("[\\W]+");
        for (final String word : splitContent) {
            String stemWord = porterStemmer.stem(word);
            if (allWordsContain(stemWord)) {
                ArrayList<String> container = words.get(stemWord);
                if (!container.contains(docName))
                    container.add(docName);
            } else {
                ArrayList<String> container = new ArrayList<>();
                container.add(docName);
                words.put(stemWord, container);
            }
        }
    }

    public boolean allWordsContain(final String word) {
        return words.containsKey(word);
    }

    public ArrayList<String> getDocsContain(final String word){
        return words.get(word);
    }

    public static HashInvertedIndex getInstance() {
        if(instance == null)
            instance = new HashInvertedIndex();
        return instance;
    }
}