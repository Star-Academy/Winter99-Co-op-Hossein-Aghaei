import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Scanner;

public final class DocsFileReader {
    private static DocsFileReader instance;

    public HashMap<String, String> scanDocs(final File[] fileList) {
        HashMap<String, String> docs = new HashMap<>();
        for (final File doc : fileList) {
            try {
                Scanner fileReader = new Scanner(doc);
                if (fileReader.hasNext())
                    docs.put(doc.getName(), fileReader.nextLine().toLowerCase());
            } catch (FileNotFoundException e) {
                System.out.println("An unknown error occurred in reading data");
            }
        }
        return docs;
    }

    public static DocsFileReader getInstance() {
        if (instance == null)
            instance = new DocsFileReader();
        return instance;
    }

}