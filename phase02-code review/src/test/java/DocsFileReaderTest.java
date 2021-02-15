import org.junit.*;
import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Objects;


public class DocsFileReaderTest {

    private DocsFileReader docsFileReader;
    @Before
    public void setUp(){
       // docsFileReader = new DocsFileReader();
    }

    @Test
    public void ShouldReturnDocs(){
        HashMap<String, String> result = new HashMap<>(){
            {
                put("57110", "I have a 42 yr old male friend, misdiagnosed as havin osteopporosis for two years, who recently found out that hi illness is the rare Gaucher's disease.Gaucher's disease symptoms include: brittle bones (he lost 9 inches off his hieght); enlarged liver and spleen; interna bleeding; and fatigue (all the time). The problem (in Type 1) i attributed to a genetic mutation where there is a lack of th enzyme glucocerebroside in macrophages so the cells swell up This will eventually cause deathEnyzme replacement therapy has been successfully developed an approved by the FDA in the last few years so that those patient administered with this drug (called Ceredase) report a remarkabl improvement in their condition. Ceredase, which is manufacture by biotech biggy company--Genzyme--costs the patient $380,00 per year. Gaucher\\'s disease has justifyably been called \"the mos expensive disease in the world\"NEED INFOI have researched Gaucher's disease at the library but am relyin on netlanders to provide me with any additional information**news, stories, report**people you know with this diseas**ideas, articles about Genzyme Corp, how to get a hold o   enough money to buy some, programs available to help wit   costs**Basically ANY HELP YOU CAN OFFEThanks so very muchDeborah".toLowerCase());
                put("58043", ">This wouldn't happen to be the same thing as chiggers, would it>A truly awful parasitic affliction, as I understand it.  Tiny bug>dig deeply into the skin, burying themselves.  Yuck!  They have thes>things in OklahomaClose. My mother comes from Gainesville Tex, right across the borderThey claim to be the chigger capitol of the world, and I believe themWhen I grew up in Fort Worth it was bad enough, but in Gainesvillin the summer an attack was guaranteedDoug McDonal".toLowerCase());
            }
        };
        //Assert.assertEquals(result, docsFileReader.scanDocs(Objects.requireNonNull(new File("Temple").listFiles())));
    }

    @Test
    public void shouldReturnObject() {
       // Assert.assertNotNull(DocsFileReader.getInstance());
    }
}
