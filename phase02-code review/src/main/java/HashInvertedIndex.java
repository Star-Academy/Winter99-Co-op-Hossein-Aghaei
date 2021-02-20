import java.util.ArrayList;
import java.util.HashMap;

public class HashInvertedIndex implements InvertedIndex {
    private final HashMap<String, ArrayList<String>> words;

    public HashInvertedIndex(HashMap<String, ArrayList<String>> words) {
        this.words = words;
    }

    public boolean contain(final String word) {
        return words.containsKey(word);
    }

    public ArrayList<String> getDocsContain(final String word) {
        return words.getOrDefault(word, new ArrayList<>());
    }
}