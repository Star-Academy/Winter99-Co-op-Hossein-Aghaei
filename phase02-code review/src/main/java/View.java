import java.util.ArrayList;

public interface View {
    void run();
    void showResult(final ArrayList<String> finalSet);
    void scanInput();
    void splitSearchKeyIntoDocs(final String[] sentence);
}