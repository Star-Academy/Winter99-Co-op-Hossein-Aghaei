using System.Collections.Generic;
using System.Linq;

namespace search
{
    public class HashInvertedIndex : IInvertedIndex
    {
        private readonly Dictionary<string, HashSet<string>> _words;
        
        public HashInvertedIndex(Dictionary<string, HashSet<string>> words)
        {
            _words = words;
        }

        public bool TryGetDocsContain(string word , out HashSet<string> result)
        {
            return _words.TryGetValue(word, out result);
        }
    }
}