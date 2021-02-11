import java.util.ArrayList;

public interface InvertedIndex {
    void organizeDocsAndWords();
    boolean allWordsContain(final String word);
    ArrayList<String> getDocsContain(final String word);
}