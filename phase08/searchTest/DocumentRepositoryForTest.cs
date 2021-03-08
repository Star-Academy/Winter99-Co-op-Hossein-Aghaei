using System.Collections.Generic;
using System.Linq;
using Model;
using search;

namespace searchTest
{
    public class DocumentRepositoryForTest : IDocumentRepository
    {
        private readonly Dictionary<string, HashSet<string>> _allData = new();

        public bool ContainsWord(string word)
        {
            return _allData.ContainsKey(word);
        }

        public IEnumerable<string> GetExistedDocs(List<string> docs)
        {
            return docs.Where(x => _allData.ContainsKey(x));
        }

        public void AddNewDoc(Doc doc)
        {
            foreach (var word in doc.WordsOfDoc)
            {
                _allData.Add(word.Term, new HashSet<string>(){doc.Name});
            }
        }

        public void AddDuplicateWords(List<string> words, Doc newDoc)
        {
            foreach (var word in words)
            {
                _allData[word].Add(newDoc.Name);
            }
        }

        public HashSet<string> GetDocsContain(string word)
        {
            return _allData.ContainsKey(word) ? _allData[word] : new HashSet<string>();
        }
    }
}