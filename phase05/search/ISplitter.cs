namespace search
{
    public interface ISplitter
    {
        DocContainer SplitInputIntoSeparateDocs(string inputSentence);
    }
}