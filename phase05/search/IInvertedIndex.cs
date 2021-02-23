using System.Collections.Generic;

namespace search
{
    public interface IInvertedIndex
    {
        bool TryGetDocsContain(string word, out HashSet<string> result);
    }
}