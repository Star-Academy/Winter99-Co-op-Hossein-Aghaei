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

        public IEnumerable<string> GetExistingDocs(List<string> docs)
        {
            return _searchContext.Docs.Where(x => docs.Contains(x.Name))
                .Select(x => x.Name).ToList();
        }

        public void AddNewDoc(Doc doc)
        {
            _searchContext.Docs.Add(doc);
        }
        
        public void AddDuplicateWords(List<string> duplicateWords, Doc newDoc)
        {
            var duplicateWordsInDb = _searchContext.Words.Where(x => duplicateWords.Contains(x.Term));
            foreach (var word in duplicateWordsInDb)
            {
                word.DocsContainer.Add(newDoc);
            }
            _searchContext.SaveChanges();
        }

        public HashSet<string> GetDocsContain(string word)
        {
            var existingWord = _searchContext.Words.Include(x => x.DocsContainer)
                .FirstOrDefault(x => x.Term == word);
            if (existingWord == default)
                return new HashSet<string>();

            return existingWord.DocsContainer.Select(x => x.Name)
                .ToHashSet();
        }
    }
}