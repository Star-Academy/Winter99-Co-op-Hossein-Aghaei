import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.when;

public class IndexCreatorTest {
    private IndexCreator indexCreator;
    private DocsFileReader docsFileReader;
    private final HashMap<String, String> expected = new HashMap<>();
    private final HashMap<String, ArrayList<String>> result = new HashMap<>();

    @Before
    public void setUp(){
        docsFileReader = mock(DocsFileReader.class);
        indexCreator = new IndexCreator(docsFileReader);
        expected.put("11111", "hello my friend");
        expected.put("22222", "hello age 21");
        ArrayList<String> fDoc = new ArrayList<>();
        fDoc.add("11111");
        ArrayList<String> sDoc = new ArrayList<>();
        sDoc.add("22222");
        ArrayList<String> tDoc = new ArrayList<>();
        tDoc.add("11111");tDoc.add("22222");
        result.put("hello", tDoc);
        result.put("my", fDoc);
        result.put("friend", fDoc);
        result.put("ag", sDoc);
        result.put("21", sDoc);
    }

    @Test
    public void shouldOrganizeData(){
        when(docsFileReader.scanDocs()).thenReturn(expected);
        Assert.assertEquals(result, indexCreator.organizeDocsAndWords());
    }

}
