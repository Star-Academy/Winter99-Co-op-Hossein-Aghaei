import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;

import static org.mockito.Mockito.*;

public class ProcessorTest {
    private final ResultDocs resultDocs = mock(ResultDocs.class);
    private final Processor processor = new Processor(resultDocs);
    private final ArrayList<ArrayList<String>> sentenceToSearch = new ArrayList<>();
    private final ArrayList<String> noSignDocs = new ArrayList<>();
    private final ArrayList<String> plusDocs = new ArrayList<>();
    private final ArrayList<String> minusDocs = new ArrayList<>();
    private final ArrayList<String> result = new ArrayList<>();

    @Before
    public void setUp(){
        ArrayList<String> noSignWords = new ArrayList<>();noSignWords.add("hello");
        ArrayList<String> plusWords = new ArrayList<>(); plusWords.add("see");plusWords.add("god");
        ArrayList<String> minusWords = new ArrayList<>();minusWords.add("goal");
        sentenceToSearch.add(noSignWords);sentenceToSearch.add(plusWords);sentenceToSearch.add(minusWords);
        noSignDocs.add("11111");noSignDocs.add("22222");noSignDocs.add("33333");noSignDocs.add("44444");noSignDocs.add("55555");
        plusDocs.add("11111");plusDocs.add("33333");plusDocs.add("44444");plusDocs.add("66666");plusDocs.add("77777");
        minusDocs.add("11111");minusDocs.add("88888");minusDocs.add("22222");minusDocs.add("99999");
        result.add("11111");
    }

    @Test
    public void shouldSearch(){
        doNothing().when(resultDocs).setNoSignDocs(any());
        doNothing().when(resultDocs).setPlusDocs(any());
        doNothing().when(resultDocs).setMinusDocs(any());
        when(resultDocs.getNoSignDocs()).thenReturn(noSignDocs);
        when(resultDocs.getPlusDocs()).thenReturn(plusDocs);
        when(resultDocs.getMinusDocs()).thenReturn(minusDocs);
        Assert.assertEquals(result, processor.search(sentenceToSearch));
    }
}
