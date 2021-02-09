import opennlp.tools.stemmer.PorterStemmer;
import java.util.ArrayList;
import java.util.Scanner;

public class ConsoleView implements View{
    private static ConsoleView instance;
    private final Processor processor;
    private final PorterStemmer porterStemmer;
    private final Scanner scanner;
    private final ArrayList<String> withoutSignWords;
    private final ArrayList<String> plusWords;
    private final ArrayList<String> minusWords;

    public ConsoleView(){
        instance = this;
        processor = Processor.getInstance();
        porterStemmer = new PorterStemmer();
        scanner = new Scanner(System.in);
        withoutSignWords = new ArrayList<>();
        plusWords = new ArrayList<>();
        minusWords = new ArrayList<>();

    }

    public void run() {
        scanInput();
        ArrayList<String> finalSet = new ArrayList<>(processor.getFinalSet());
        if (finalSet.isEmpty())
            System.out.println("No Match Found!");
        else {
            System.out.println("Total number of Docs is : " + finalSet.size());
            for (final String doc : finalSet)
                System.out.println(doc);
        }
    }

    private void scanInput() {
        System.out.println("Enter your sentence to search!");
        String search = scanner.nextLine().toLowerCase();
        String[] splitInput = search.split("\\s");
        for (final String word : splitInput) {
            if (word.startsWith("+"))
                plusWords.add(porterStemmer.stem(word.replace("+", "")));
            else if (word.startsWith("-"))
                minusWords.add(porterStemmer.stem(word.replace("-", "")));
            else
                withoutSignWords.add(porterStemmer.stem(word));
        }
    }

    public ArrayList<String> getPlusWords() {
        return plusWords;
    }

    public ArrayList<String> getMinusWords() {
        return minusWords;
    }

    public ArrayList<String> getWithoutSignWords() {
        return withoutSignWords;
    }

    public static ConsoleView getInstance() {
        if (instance == null)
            instance = new ConsoleView();
        return instance;
    }
}