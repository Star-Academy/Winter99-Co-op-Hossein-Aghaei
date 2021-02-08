import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Scanner;

public final class FileReader {
    private static FileReader instance;
    private final HashMap<String, String> docs;
    private final File[] fillList;

    public FileReader() {
        docs = new HashMap<String, String>();
        File file = new File("..\\EnglishData");
        fillList = file.listFiles();
    }

    public void scanDocs() throws FileNotFoundException {
        for (final File doc : fillList) {
            Scanner fileReader = new Scanner(doc);
            if (fileReader.hasNext())
                docs.put(doc.getName(), fileReader.nextLine().toLowerCase());
        }
    }

    public HashMap<String, String> getDocs() {
        return docs;
    }

    public static FileReader getInstance() {
        if (instance == null)
            instance = new FileReader();
        return instance;
    }
}
