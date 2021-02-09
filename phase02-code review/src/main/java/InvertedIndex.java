import java.util.ArrayList;

public interface InvertedIndex {
    void organize();
    boolean allWordsContain(final String word);
    ArrayList<String> getDocsContain(final String word);
}