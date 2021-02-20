import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;

import java.util.ArrayList;
import java.util.HashMap;

import static org.mockito.Mockito.*;

public class ResultDocsTest {
    private final HashInvertedIndex hashInvertedIndex = mock(HashInvertedIndex.class);
    private final DocsFileReader docsFileReader = mock(DocsFileReader.class);
    private final ResultDocs resultDocs = new ResultDocs(hashInvertedIndex, docsFileReader);
    private HashMap<String, String> allDocs;


    @Test
    public void shouldGetNoSignDocs(){
        ArrayList<String> container = new ArrayList<>(); container.add("58045"); container.add("58046");
        ArrayList<String> noSignWords = new ArrayList<>(); noSignWords.add("see"); noSignWords.add("OTC");noSignWords.add("fuck");
        when(hashInvertedIndex.contain("see")).thenReturn(true);
        when(hashInvertedIndex.getDocsContain("see")).thenReturn(container);
        container.remove("58045");
        when(hashInvertedIndex.getDocsContain("OTC")).thenReturn(container);
        when(hashInvertedIndex.contain("OTC")).thenReturn(true);
        when(hashInvertedIndex.contain("fuck")).thenReturn(false);
        container.clear();
        resultDocs.setNoSignDocs(noSignWords);
        Assert.assertEquals(container, resultDocs.getNoSignDocs());
    }

    @Test
    public void shouldGetPlusDocs(){
        ArrayList<String> plusWords = new ArrayList<>(); plusWords.add("see"); plusWords.add("friend");plusWords.add("skin");
        ArrayList<String> fContainer = new ArrayList<>();fContainer.add("58045");fContainer.add("58046");
        when(hashInvertedIndex.contain("see")).thenReturn(true);
        when(hashInvertedIndex.getDocsContain("see")).thenReturn(fContainer);
        ArrayList<String> sContainer = new ArrayList<>();sContainer.add("57110");
        when(hashInvertedIndex.contain("friend")).thenReturn(true);
        when(hashInvertedIndex.getDocsContain("friend")).thenReturn(sContainer);
        when(hashInvertedIndex.contain("skin")).thenReturn(false);
        ArrayList<String> result = new ArrayList<>(); result.addAll(fContainer); result.addAll(sContainer);
        resultDocs.setPlusDocs(plusWords);
        Assert.assertEquals(result, resultDocs.getPlusDocs());
    }

    @Before
    public void setUp(){
        allDocs = new HashMap<>(){
            {
                put("57110", "I have a 42 yr old male friend, misdiagnosed as havin osteopporosis for two years, who recently found out that hi illness is the rare Gaucher's disease.Gaucher's disease symptoms include: brittle bones (he lost 9 inches off his hieght); enlarged liver and spleen; interna bleeding; and fatigue (all the time). The problem (in Type 1) i attributed to a genetic mutation where there is a lack of th enzyme glucocerebroside in macrophages so the cells swell up This will eventually cause deathEnyzme replacement therapy has been successfully developed an approved by the FDA in the last few years so that those patient administered with this drug (called Ceredase) report a remarkabl improvement in their condition. Ceredase, which is manufacture by biotech biggy company--Genzyme--costs the patient $380,00 per year. Gaucher\\'s disease has justifyably been called \"the mos expensive disease in the world\"NEED INFOI have researched Gaucher's disease at the library but am relyin on netlanders to provide me with any additional information**news, stories, report**people you know with this diseas**ideas, articles about Genzyme Corp, how to get a hold o   enough money to buy some, programs available to help wit   costs**Basically ANY HELP YOU CAN OFFEThanks so very muchDeborah".toLowerCase());
                put("58043", ">This wouldn't happen to be the same thing as chiggers, would it>A truly awful parasitic affliction, as I understand it.  Tiny bug>dig deeply into the skin, burying themselves.  Yuck!  They have thes>things in OklahomaClose. My mother comes from Gainesville Tex, right across the borderThey claim to be the chigger capitol of the world, and I believe themWhen I grew up in Fort Worth it was bad enough, but in Gainesvillin the summer an attack was guaranteedDoug McDonal".toLowerCase());
                put("58045", ">>>Another uncommon problem is maternal hemorrhage.  I don't remember th>>incidence, but it is something like 1 in 1,000 or 10,000 births.  It is har>>to see how you could handle it at home, and you wouldn't have very much time>>>thing you might consider is that people's risk tradeoffs vary.  I conside>>a 1/1,000 risk of loss of a loved one to require considerable effort i>>the avoiding>Mark, you seem to be terrified of the birth procesThat's ridiculous>and unable t>believe that women's bodies are actually designed to do itThey aren't designed, they evolved.  And, much as it discomforts us, ihumans a trouble-free birth process was sacrificed to increased brain ancranial size.  Wild animals have a much easier time with birth than humans doDomestic horses and cows typically have a worse time.  To give you an ideamy family tree is complicated because a few of my pioneer great-greatgrandfathers had several wives, and we never could figure out which wifhad each child.  One might ask why this happened.  My great-greatgrandfathers were, by the time they reached their forties, quite prosperoufarmers.  Nonetheless, they lost several wives each to the rigors ochildbirth; the graveyards in Spencer, Indiana, and Boswell, North Dakotacontain quite a few gravestones like \"Ida, wf. of Jacob Liptrap, anbaby, May 6, 1853.>You wante>to section all women carrying breech in case one in a hundred or >thousand breech babies get hung up in second stageMore like one in ten.  And the consequences can be devastating; I havdirect experience of more than a dozen victims of a fouled-up breech birth>and now you wan>all babies born in hospital based on a guess of how likely materna>hemorrhage is and a false belief that it is fatalIt isn't always fatal.  But it is often fatal, when it happens out oreach of adequate help.  More often, it permanently damages one's healthClearly women's bodies _evolved_ to give birth (I am no believer in divindesign); however, evolution did not favor trouble-free births for humans. >You have your kids where you want. You encourage your wife t>get six inch holes cut through her stomach muscles, expose hersel>to anesthesia and infection, and whatever other \"just in case\" measure>you think are necessaryMy, aren't we wroth!  I haven't read a more outrageous straw man attacin months!  I can practically see your mouth foamWe're statistically sophisticated enough to balance the risks.  AlthougI can't produce exact statistics 5 years after the last time we lookethem up, rest assured that we balanced C-section risks against other risksI wouldn't encourage my wife to have a Caesarean unless it was clearlindicated; on the other hand, I am opposed (on obvious grounds) to waitinuntil an emergency to give inAnd bear this in mind: my wife took the lead in all of these decisionsWe talked things over, and I did a lot of the leg work, but the maidecisions were really hers>But I for one am bothered by your continue>suggestions, especially to the misc.kidders pregnant for the firs>time, that birth is dangerous, even fatal, and that all thes>unpleasant things are far better than the risks you run just doin>it naturallyI don't know of very many home birth advocates, even, that think thaa first-time mother should have her baby at home>I'm no Luddite. I've had a section. I'm planning a hospital birt>this time. But for heaven's sake, not everyone needs thatBut people should bother to find out the relative risks.  My wife waunwilling to take any significant risks in order to have nice surroundingsIn view of the intensity of the birth experience, I doubt surroundinghave much importance anyway.  Somehow the values you're advocating seeall lopsided to me: taking risks, even if fairly small, of serioupermanent harm in order to preserve something that is, after allan esthetic consideration--Mark A. Fulk\\t\\t\\tUniversity of RochesteComputer Science Department\\tfulk@cs.rochester.ed");
                put("58046", "I sometimes see OTC preparations for muscle aches/back aches thacombine aspirin with a diuretic. The idea seems to be to reducinflammation by getting rid of fluid. Does this actually work?Thanks-Larry C.");
            }
        };
    }

    @Test
    public void shouldGetMinusDocs(){
        when(docsFileReader.scanDocs()).thenReturn(allDocs);
        ArrayList<String> minusWords = new ArrayList<>(); minusWords.add("see"); minusWords.add("fuck");
        ArrayList<String> fContainer = new ArrayList<>();fContainer.add("58045");fContainer.add("58046");
        when(hashInvertedIndex.contain("see")).thenReturn(true);
        when(hashInvertedIndex.getDocsContain("see")).thenReturn(fContainer);
        when(hashInvertedIndex.contain("fuck")).thenReturn(false);
        ArrayList<String> result = new ArrayList<>();result.add("58043");result.add("57110");
        resultDocs.setMinusDocs(minusWords);
        Assert.assertEquals(result, resultDocs.getMinusDocs());
    }
/*
    @Test
    public void shouldGetFinalNoSignDocs(){
        ArrayList<String> noSignWords = new ArrayList<>();
        ArrayList<String> result = new ArrayList<>();
        HashInvertedIndex hashInvertedIndex = HashInvertedIndex.getInstance();
        hashInvertedIndex.organizeDocsAndWords();
        Assert.assertEquals(result, resultDocs.getFinalNoSignDocs(noSignWords));
        noSignWords.add("lock");
        result.add("58766");
        Assert.assertEquals(result, resultDocs.getFinalNoSignDocs(noSignWords));
        noSignWords.add("mechanic");
        result.clear();
        Assert.assertEquals(result, resultDocs.getFinalNoSignDocs(noSignWords));
    }*/
}
