import java.io.FileNotFoundException;

public class Main {
    public static void main(String[] args) {
        View view = View.getInstance();
        try {
            view.run();
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
}