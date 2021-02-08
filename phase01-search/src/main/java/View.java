import opennlp.tools.stemmer.PorterStemmer;

import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Scanner;

public class View {
    private static View instance;
    private final PorterStemmer porterStemmer;
    private final FileReader fileReader;
    private final InvertedIndex invertedIndex;
    private final Scanner scanner;
    private final ArrayList<String> withoutSignWords;
    private final ArrayList<String> plusWords;
    private final ArrayList<String> minusWords;

    public View() {
        porterStemmer = new PorterStemmer();
        fileReader = FileReader.getInstance();
        invertedIndex = InvertedIndex.getInstance();
        scanner = new Scanner(System.in);
        withoutSignWords = new ArrayList<>();
        plusWords = new ArrayList<>();
        minusWords = new ArrayList<>();

    }

    public void run() throws FileNotFoundException {
        fileReader.scanDocs();
        invertedIndex.organize();
        System.out.println("Enter your sentence to search!");
        String search = scanner.nextLine().toLowerCase();
        String[] split = search.split("\\s");
        for (final String word : split) {
            if (word.startsWith("+"))
                plusWords.add(porterStemmer.stem(word.replace("+", "")));
            else if (word.startsWith("-"))
                minusWords.add(porterStemmer.stem(word.replace("-", "")));
            else
                withoutSignWords.add(porterStemmer.stem(word));
        }
        ArrayList<String> finalSet = new ArrayList<String>(getFinalSet());
        if (finalSet.isEmpty())
            System.out.println("No Match Found!");
        else {
            System.out.println("Total number of Docs is : " + finalSet.size());
            for (final String doc : finalSet)
                System.out.println(doc);
        }
    }

    private ArrayList<String> getFinalSet() {
        ArrayList<String> finalSet = new ArrayList<String>();
        if (withoutSignWords.isEmpty()) {
            if (plusWords.isEmpty()) {
                finalSet.addAll(getMinusDocs());
            } else
                finalSet.addAll(getPlusDocs());
        } else
            finalSet.addAll(getWithoutSignDocs());
        ArrayList<String> minusSet = new ArrayList<>(getMinusDocs());
        finalSet.removeIf(doc -> !minusSet.contains(doc));
        ArrayList<String> plusSet = new ArrayList<String>(getPlusDocs());
        finalSet.removeIf(doc -> !plusSet.contains(doc) && !plusWords.isEmpty());
        return finalSet;
    }

    private ArrayList<String> getWithoutSignDocs() {
        ArrayList<String> docs = new ArrayList<String>();
        if (!withoutSignWords.isEmpty()) {
            if (invertedIndex.getWords().containsKey(withoutSignWords.get(0))) {
                docs.addAll(invertedIndex.getWords().get(withoutSignWords.get(0)));
            } else
                return docs;
        } else
            return docs;
        for (int i = 1; i < withoutSignWords.size(); i++) {
            if (invertedIndex.getWords().containsKey(withoutSignWords.get(i))) {
                ArrayList<String> docsOfWord = new ArrayList<String>(invertedIndex.getWords().get(withoutSignWords.get(i)));
                docs.removeIf(doc -> !docsOfWord.contains(doc));
            } else {
                docs.clear();
                return docs;
            }
        }
        return docs;
    }

    private ArrayList<String> getPlusDocs() {
        ArrayList<String> docs = new ArrayList<String>();
        for (final String word : plusWords) {
            if (invertedIndex.getWords().containsKey(word))
                docs.addAll(invertedIndex.getWords().get(word));
        }
        return docs;
    }

    private ArrayList<String> getMinusDocs() {
        ArrayList<String> docs = new ArrayList<String>(fileReader.getDocs().keySet());
        for (final String word : minusWords) {
            if (invertedIndex.getWords().containsKey(word))
                docs.removeAll(invertedIndex.getWords().get(word));
        }
        return docs;
    }

    public static View getInstance() {
        if (instance == null)
            instance = new View();
        return instance;
    }
}