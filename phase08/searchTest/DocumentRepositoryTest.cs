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
            _searchContext = new DbContextFactory().CreateContext();
        }

        [Fact]
        public void GetExistedDocs_ShouldReturnEmptyList_WhenNoInputExists()
        {
            var documentRepository = new DocumentRepository(_searchContext);
            var result = documentRepository.GetExistedDocs(new List<string>() {"356", "782"}).ToList();
            Assert.Empty(result);
        }

        public void Dispose()
        {
            _searchContext?.Dispose();
        }
    }
}