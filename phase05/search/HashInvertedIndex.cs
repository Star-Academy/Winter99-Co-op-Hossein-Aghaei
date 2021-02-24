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
        
        public HashSet<string> GetDocsContain(string word)
        {
            return !_words.ContainsKey(word) ? new HashSet<string>() : _words[word];
        }
    }
}