import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Scanner;

public final class FileReader {
    private static FileReader instance;
    private final HashMap<String, String> docs;
    private final File[] fillList;

    public FileReader(){
        docs = new HashMap<>();
        fillList = new File("C:\\Users\\hos3in\\Desktop\\phase02-code review\\EnglishData").listFiles();
        scanDocs();
    }

    public void scanDocs(){
        for (final File doc : fillList) {
            try {
                Scanner fileReader = new Scanner(doc);
                if (fileReader.hasNext())
                    docs.put(doc.getName(), fileReader.nextLine().toLowerCase());
            } catch (FileNotFoundException e) {
                System.out.println("An unknown error occurred in reading data");
            }
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