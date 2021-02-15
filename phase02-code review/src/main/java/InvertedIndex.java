import java.util.ArrayList;

public interface InvertedIndex {
    boolean contain(final String word);
    ArrayList<String> getDocsContain(final String word);
}