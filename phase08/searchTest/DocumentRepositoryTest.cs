using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using search;
using Xunit;

namespace searchTest
{
    public class DocumentRepositoryTest : IDisposable
    {
        private readonly SearchContext _searchContext;

        public DocumentRepositoryTest()
        {
            _searchContext = DbContextFactory.CreateContext();
        }

        [Fact]
        public void GetExistingDocs_ShouldReturnEmptyList_WhenNoInputExists()
        {
            var documentRepository = new DocumentRepository(_searchContext);
            var result = documentRepository.GetExistingDocs(new List<string>() {"356", "782"}).ToList();
            Assert.Empty(result);
        }
        
        [Fact]
        public void GetExistingDocs_ShouldReturnCorrectList_WhenInputExists()
        {
            var expected = new List<string>() {"123"};

            var documentRepository = new DocumentRepository(_searchContext);
            var result = documentRepository.GetExistingDocs(new List<string>() {"123", "782"}).ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ContainsWord_ShouldReturnTrue_WhenWordExistsInDb()
        {
            var documentRepository = new DocumentRepository(_searchContext);
            var result = documentRepository.ContainsWord("hello");
            Assert.True(result);
        }

        [Fact]
        public void AddNewDoc_ShouldAddOneDoc()
        {
            var doc = new Doc() {Name = "789", Content = "hello home"};
            doc.WordsOfDoc.Add(new Word() {Term = "home"});

            var documentRepository = new DocumentRepository(_searchContext);
            documentRepository.AddNewDoc(doc);
            documentRepository.AddDuplicateWords(new List<string>(), new Doc()); // For calling SaveChanges()

            Assert.Equal(3, _searchContext.Docs.Count());
            Assert.True(_searchContext.Docs.Any(x => x.Name == "789"));
        }

        [Fact]
        public void AddDuplicateWords_ShouldAddOneAnotherDocToExistingWordDocContainer()
        {
            var doc = new Doc() {Name = "789", Content = "hello home"};
            doc.WordsOfDoc.Add(new Word() {Term = "home"});
            var words = new List<string>() {"hello"};

            var documentRepository = new DocumentRepository(_searchContext);
            documentRepository.AddDuplicateWords(words, doc);
            var expected = _searchContext.Words.Where(x => x.Term == "hello")
                .Include(x => x.DocsContainer)
                .First().DocsContainer;

            Assert.Equal(3, expected.Count);
            Assert.Equal("789", expected[2].Name);
        }

        [Fact]
        public void GetDocsContain_ShouldReturnCorrectDocs_WhenInputExists()
        {
            var expected = new HashSet<string>() {"123", "456"};

            var documentRepository = new DocumentRepository(_searchContext);
            var result = documentRepository.GetDocsContain("child");

            Assert.Equal(expected, result);
        }

        public void Dispose()
        {
            _searchContext?.Dispose();
        }
    }
}