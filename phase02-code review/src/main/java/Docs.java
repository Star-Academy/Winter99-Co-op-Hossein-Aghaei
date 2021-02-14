import java.util.ArrayList;

abstract public class Docs {
    protected ArrayList<String> noSignDocs = new ArrayList<>();
    protected ArrayList<String> plusDocs = new ArrayList<>();
    protected ArrayList<String> minusDocs = new ArrayList<>();


    public abstract ArrayList<String> getNoSignDocs();

    public abstract void setNoSignDocs(final ArrayList<String> noSignDocs);

    public abstract ArrayList<String> getPlusDocs();

    public abstract void setPlusDocs(final ArrayList<String> plusDocs);

    public abstract ArrayList<String> getMinusDocs();

    public abstract void setMinusDocs(final ArrayList<String> minusDocs);
}
