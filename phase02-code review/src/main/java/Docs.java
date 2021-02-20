import java.util.ArrayList;

public interface Docs {
    ArrayList<String> getNoSignDocs();
    ArrayList<String> getPlusDocs();
    ArrayList<String> getMinusDocs();
    void setNoSignDocs(ArrayList<String> noSignWords);
    void setPlusDocs(ArrayList<String> plusSignWords);
    void setMinusDocs(ArrayList<String> minusWords);
}