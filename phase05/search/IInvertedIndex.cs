using System.Collections.Generic;

namespace search
{
    public interface IInvertedIndex
    {
        HashSet<string> GetDocsContain(string word);
    }
}