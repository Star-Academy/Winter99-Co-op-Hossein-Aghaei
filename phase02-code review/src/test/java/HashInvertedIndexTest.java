import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import java.util.ArrayList;
import java.util.HashMap;

public class HashInvertedIndexTest {
    private final HashMap<String, ArrayList<String>> words = new HashMap<>();
    private HashInvertedIndex hashInvertedIndex;

    @Before
    public void setUp(){
        ArrayList<String> fDoc = new ArrayList<>();
        fDoc.add("11111");
        ArrayList<String> sDoc = new ArrayList<>();
        sDoc.add("22222");
        ArrayList<String> tDoc = new ArrayList<>();
        tDoc.add("11111");tDoc.add("22222");
        words.put("hello", tDoc);
        words.put("my", fDoc);
        words.put("friend", fDoc);
        words.put("ag", sDoc);
        words.put("21", sDoc);
        hashInvertedIndex = new HashInvertedIndex(words);
    }

    @Test
    public void shouldContainSomeWords(){
        Assert.assertTrue(hashInvertedIndex.contain("friend"));
        Assert.assertFalse(hashInvertedIndex.contain("dog"));
        ArrayList<String> tDoc = new ArrayList<>();
        Assert.assertEquals(new ArrayList<>(), hashInvertedIndex.getDocsContain("dad"));
        tDoc.add("11111");tDoc.add("22222");
        Assert.assertEquals(tDoc, hashInvertedIndex.getDocsContain("hello"));
    }
}