using System.Collections.Generic;
using Model;

namespace search
{
    public interface IDocumentRepository
    {
        bool ContainsWord(string word);

        IEnumerable<string> GetExistingDocs(List<string> docs);

        void AddNewDoc(Doc doc);

         void AddDuplicateWords(List<string> words, Doc newDoc);
        
        HashSet<string> GetDocsContain(string word);
    }
}