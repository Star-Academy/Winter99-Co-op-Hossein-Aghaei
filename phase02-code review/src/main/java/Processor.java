import java.util.ArrayList;

public class Processor {
    private static Processor instance;
    private final ResultDocs resultDocs = ResultDocs.getInstance();

    public ArrayList<String> getFinalSet() {
        ArrayList<String> finalSet = resultDocs.getInitialFinalSet();
        finalSet.removeIf(doc -> !resultDocs.getMinusDocs().contains(doc));
        finalSet.removeIf(doc -> !resultDocs.getPlusDocs().contains(doc) && !resultDocs.getPlusDocs().isEmpty());
        return finalSet;
    }

    public static Processor getInstance() {
        if (instance == null)
            instance = new Processor();
        return instance;
    }
}