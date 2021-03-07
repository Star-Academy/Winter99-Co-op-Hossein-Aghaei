using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace search
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly SearchContext _searchContext;

        public DocumentRepository(SearchContext searchContext)
        {
            _searchContext = searchContext;
        }

        public bool ContainsWord(string word)
        {
            return _searchContext.Words.Any(x => x.Term == word);
        }

        public IEnumerable<string> GetExistedDocs(List<string> docs)
        {
            return _searchContext.Docs.Where(x => docs.Contains(x.Name)).Select(x => x.Name).ToList();
        }

        public void AddNewDoc(Doc doc)
        {
            _searchContext.Docs.Add(doc);
        }
        
        public void AddDuplicateWords(List<string> duplicateWords, Doc newDoc)
        {
            var xWord = _searchContext.Words.Where(x => duplicateWords.Contains(x.Term));
            foreach (var word in xWord)
            {
                word.DocsContainer.Add(newDoc);
            }
            _searchContext.SaveChanges();
        }

        public HashSet<string> GetDocsContain(string word)
        {
            return _searchContext.Words.Any(x => x.Term == word)
                ? _searchContext.Words.Include(x => x.DocsContainer).First(x => x.Term == word).DocsContainer.Select(x => x.Name).ToHashSet()
                : new HashSet<string>();
        }
    }
}