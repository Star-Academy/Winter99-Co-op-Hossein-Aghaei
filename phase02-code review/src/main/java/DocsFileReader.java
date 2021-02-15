import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Scanner;

public class DocsFileReader implements Config, FileReader{
    private final String docsPath;
    private final HashMap<String, String> docs = new HashMap<>();
    private final Object docsLock = new Object();

    public DocsFileReader(String docsPath) {
        this.docsPath = docsPath;
    }

    public HashMap<String, String> scanDocs() {
        if (docs.isEmpty()) {
            synchronized (docsLock) {
                if (docs.isEmpty()) {
                    try {
                        for (final File doc : getDocsDirectoryFiles()) {
                            Scanner fileReader = new Scanner(doc);
                            if (fileReader.hasNext())
                                docs.put(doc.getName(), fileReader.nextLine().toLowerCase());
                        }
                    } catch (FileNotFoundException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        return docs;
    }

    private File[] getDocsDirectoryFiles() {
        return new File(getPath()).listFiles();
    }

    @Override
    public String getPath() {
        return this.docsPath;
    }
}