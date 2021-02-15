import java.util.ArrayList;

public class Controller {
    private final ConsoleView consoleView;
    private final Processor processor;

    public Controller(ConsoleView consoleView, Processor processor) {
        this.consoleView = consoleView;
        this.processor = processor;
    }

    public void run(){
        String sentence = consoleView.scanInput();
        ArrayList<ArrayList<String>> allDocs = consoleView.splitSearchKeyIntoDocs(sentence);
        consoleView.showResult(processor.search(allDocs));
    }
}
