import opennlp.tools.stemmer.PorterStemmer;
import java.util.ArrayList;
import java.util.Scanner;

public class ConsoleView implements View {
    private final PorterStemmer porterStemmer = new PorterStemmer();

    @Override
    public void showResult(final ArrayList<String> finalSet) {
        if (finalSet.isEmpty()) {
            System.out.println("No Match Found!");
            return;
        }
        System.out.println("Total number of Docs is : " + finalSet.size());
        for (final String doc : finalSet)
            System.out.println(doc);

    }

    @Override
    public String scanInput() {
        System.out.println("Enter your sentence to search!");
        Scanner scanner = new Scanner(System.in);
        return scanner.nextLine().toLowerCase();
    }

    @Override
    public ArrayList<ArrayList<String>> splitSearchKeyIntoDocs(String sentenceToSearch) {
        ArrayList<ArrayList<String>> allDocs = new ArrayList<>();
        ArrayList<String> noSignWords = new ArrayList<>();
        ArrayList<String> plusWords = new ArrayList<>();
        ArrayList<String> minusWords = new ArrayList<>();
        for (final String word : sentenceToSearch.split("\\s")) {
            if (word.startsWith("+"))
                plusWords.add(porterStemmer.stem(word.replace("+", "")));
            else if (word.startsWith("-"))
                minusWords.add(porterStemmer.stem(word.replace("-", "")));
            else
                noSignWords.add(porterStemmer.stem(word));
        }
        allDocs.add(noSignWords);
        allDocs.add(plusWords);
        allDocs.add(minusWords);
        return allDocs;
    }
}