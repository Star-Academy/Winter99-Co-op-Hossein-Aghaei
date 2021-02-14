import opennlp.tools.stemmer.PorterStemmer;
import java.util.ArrayList;
import java.util.Scanner;

public class ConsoleView implements View{
    private static ConsoleView instance = ConsoleView.getInstance();
    private final Processor processor = Processor.getInstance();
    private final InputDocs inputDocs = InputDocs.getInstance();
    private final PorterStemmer porterStemmer = new PorterStemmer();

    @Override
    public void run() {
        scanInput();
        showResult(processor.getFinalSet());
    }

    @Override
    public void showResult(final ArrayList<String> finalSet) {
        if (finalSet.isEmpty())
            System.out.println("No Match Found!");
        else {
            System.out.println("Total number of Docs is : " + finalSet.size());
            for (final String doc : finalSet)
                System.out.println(doc);
        }
    }

    @Override
    public void scanInput() {
        System.out.println("Enter your sentence to search!");
        Scanner scanner = new Scanner(System.in);
        String sentenceToSearch = scanner.nextLine().toLowerCase();
        splitSearchKeyIntoDocs(sentenceToSearch.split("\\s"));
    }

    @Override
    public void splitSearchKeyIntoDocs(String[] sentenceToSearch) {
        ArrayList<String> noSignWords = new ArrayList<>();
        ArrayList<String> plusWords = new ArrayList<>();
        ArrayList<String> minusWords = new ArrayList<>();
        for (final String word : sentenceToSearch) {
            if (word.startsWith("+"))
                plusWords.add(porterStemmer.stem(word.replace("+", "")));
            else if (word.startsWith("-"))
                minusWords.add(porterStemmer.stem(word.replace("-", "")));
            else
                noSignWords.add(porterStemmer.stem(word));
        }
        inputDocs.setNoSignDocs(noSignWords);
        inputDocs.setPlusDocs(plusWords);
        inputDocs.setMinusDocs(minusWords);
    }

    public static ConsoleView getInstance() {
        if (instance == null)
            instance = new ConsoleView();
        return instance;
    }
}