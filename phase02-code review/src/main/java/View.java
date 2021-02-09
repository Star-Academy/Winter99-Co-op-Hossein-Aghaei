import java.util.ArrayList;

public interface View {
    void run();
    ArrayList<String> getPlusWords();
    ArrayList<String> getMinusWords();
    ArrayList<String> getWithoutSignWords();
}