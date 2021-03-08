using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace searchTest
{
    public static class DbContextFactory
    {
        public static SearchContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<SearchContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SearchContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var doc1 = new Doc() {Name = "123", Content = "hello to my dear child"};
            doc1.WordsOfDoc.Add(new Word() {Term = "to"});
            doc1.WordsOfDoc.Add(new Word() {Term = "my"});
            doc1.WordsOfDoc.Add(new Word() {Term = "dear"});

            var doc2 = new Doc() {Name = "456", Content = "hello lazy child"};
            doc2.WordsOfDoc.Add(new Word() {Term = "lazy"});
            
            var word1 = new Word() {Term = "hello", DocsContainer = new List<Doc>() {doc1, doc2}};
            var word2 = new Word() {Term = "child", DocsContainer = new List<Doc>() {doc1, doc2}};
            

            context.Docs.AddRange(doc1, doc2);
            context.Words.AddRange(word1, word2);
            context.SaveChanges();
            
            return context;
        }
    }
}
