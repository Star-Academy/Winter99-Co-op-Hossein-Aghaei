import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

public class ConsoleViewTest {
    private final ConsoleView consoleView = new ConsoleView();
    ArrayList<String> noSignDocs = new ArrayList<>();
    ArrayList<String> plusSignDocs = new ArrayList<>();
    ArrayList<String> minusSignDocs = new ArrayList<>();

    @Before
    public void setUp(){
        noSignDocs.add("hello");noSignDocs.add("mother");
        plusSignDocs.add("dad");
        minusSignDocs.add("ag");minusSignDocs.add("mechan");
    }

    @Test
    public void ShouldSplitSearchKey() {
        ArrayList<ArrayList<String>> allDocs = new ArrayList<>();
        allDocs.add(noSignDocs);allDocs.add(plusSignDocs);allDocs.add(minusSignDocs);
        Assert.assertEquals(allDocs, consoleView.splitSearchKeyIntoDocs("hello -age +dad -mechanic mother"));
    }

    @Test
    public void shouldRun(){
        ConsoleView consoleView = mock(ConsoleView.class);
        Processor processor = mock(Processor.class);
        when(consoleView.scanInput()).thenReturn("hello -age +dad -mechanic mother");
        when(processor.search(any())).thenReturn(new ArrayList<>());
        Controller controller = new Controller(consoleView, processor);
        controller.run();
        verify(consoleView).showResult(any());
    }
}
