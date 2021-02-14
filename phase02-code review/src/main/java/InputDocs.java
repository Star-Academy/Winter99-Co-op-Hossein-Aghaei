import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;

public class InputDocs extends Docs {
    private static InputDocs instance;
    private final DocsFileReader docsFileReader = DocsFileReader.getInstance();
    private HashMap<String, String> allDocs = new HashMap<>();
    private final Object docsLock = new Object();

    public HashMap<String, String> getAllDocs(File[] fileList) {
        if (allDocs.isEmpty()) {
            synchronized (docsLock) {
                if (allDocs.isEmpty())
                    allDocs = docsFileReader.scanDocs(fileList);
            }
        }
        return allDocs;
    }

    public File getDocsDirectoryFile() {
        return new File("C:\\Users\\hos3in\\Desktop\\Winter99-Co-op-Hossein-Aghaei\\phase02-code review\\EnglishData");
    }

    @Override
    public ArrayList<String> getNoSignDocs() {
        return noSignDocs;
    }

    @Override
    public void setNoSignDocs(ArrayList<String> noSignWords) {
        noSignDocs = noSignWords;
    }

    @Override
    public ArrayList<String> getPlusDocs() {
        return plusDocs;
    }

    @Override
    public void setPlusDocs(ArrayList<String> plusWords) {
        plusDocs = plusWords;
    }

    @Override
    public ArrayList<String> getMinusDocs() {
        return minusDocs;
    }

    @Override
    public void setMinusDocs(ArrayList<String> minusWords) {
        minusDocs = minusWords;
    }

    public static InputDocs getInstance() {
        if (instance == null)
            instance = new InputDocs();
        return instance;
    }
}