import java.util.ArrayList;

public interface DocOperation {
    ArrayList<String> getFinalNoSignDocs(final ArrayList<String> noSignWords);
    ArrayList<String> getFinalPlusDocs(final ArrayList<String> plusSignWords);
    ArrayList<String> getFinalMinusDocs(final ArrayList<String> minusSignWords);
}