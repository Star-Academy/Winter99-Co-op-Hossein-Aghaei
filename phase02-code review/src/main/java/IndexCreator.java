import opennlp.tools.stemmer.PorterStemmer;
import java.util.ArrayList;
import java.util.HashMap;

public class IndexCreator {
    private final DocsFileReader docsFileReader;
    private final PorterStemmer porterStemmer = new PorterStemmer();

    public IndexCreator(DocsFileReader docsFileReader) {
        this.docsFileReader = docsFileReader;
    }

    public HashMap<String, ArrayList<String>> organizeDocsAndWords() {
        HashMap<String, ArrayList<String>> allWords = new HashMap<>();
        HashMap<String, String> allDocs = docsFileReader.scanDocs();
        for (final String doc : allDocs.keySet()) {
            HashMap<String, String> wordsAndDocs = queryOnDocsAndWords(doc, allDocs.get(doc));
            for (String word : wordsAndDocs.keySet()) {
                if (allWords.containsKey(word))
                    allWords.get(word).add(wordsAndDocs.get(word));
                else {
                    ArrayList<String> docs = new ArrayList<>();
                    docs.add(wordsAndDocs.get(word));
                    allWords.put(word, docs);
                }
            }
        }
        return allWords;
    }

    private HashMap<String, String> queryOnDocsAndWords(final String docName, final String content) {
        HashMap<String, String> wordsAndDocs = new HashMap<>();
        String[] splitContent = content.split("[\\W]+");
        for (final String word : splitContent) {
            String stemWord = porterStemmer.stem(word);
            wordsAndDocs.putIfAbsent(stemWord, docName);
        }
        return wordsAndDocs;
    }
}
