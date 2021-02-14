import java.util.ArrayList;

public interface InvertedIndex {
    boolean allWordsContain(final String word);
    ArrayList<String> getDocsContain(final String word);
}