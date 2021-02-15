import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;

public class ConsoleViewTest {
    private final ConsoleView consoleView = new ConsoleView();
    ArrayList<String> noSignDocs = new ArrayList<>();
    ArrayList<String> plusSignDocs = new ArrayList<>();
    ArrayList<String> minusSignDocs = new ArrayList<>();
    String[] splitDataToSearch = new String[5];

    @Before
    public void setUp(){
        splitDataToSearch[0] = "hello";
        splitDataToSearch[1] = "-age";
        splitDataToSearch[2] = "+dad";
        splitDataToSearch[3] = "-mechanic";
        splitDataToSearch[4] = "mother";
        noSignDocs.add("hello"); noSignDocs.add("mother");
        plusSignDocs.add("dad");
        minusSignDocs.add("ag");minusSignDocs.add("mechan");
    }
/*
    @Test
    public void ShouldSplitSearchKey() {
        consoleView.splitSearchKeyIntoDocs(splitDataToSearch);
        Assert.assertEquals(noSignDocs, InputDocs.getInstance().getNoSignDocs());
        Assert.assertEquals(plusSignDocs, InputDocs.getInstance().getPlusDocs());
        Assert.assertEquals(minusSignDocs, InputDocs.getInstance().getMinusDocs());
    }*/
}
