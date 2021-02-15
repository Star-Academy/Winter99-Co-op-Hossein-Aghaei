import java.util.ArrayList;

public interface View {
    void run();
    void showResult(final ArrayList<String> finalSet);
    String scanInput();
    ArrayList<ArrayList<String>> splitSearchKeyIntoDocs(final String sentence);
}