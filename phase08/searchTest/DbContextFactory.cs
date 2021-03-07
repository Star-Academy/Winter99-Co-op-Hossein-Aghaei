using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Model;

namespace searchTest
{
    public class DbContextFactory
    {
        public SearchContext CreateContext()
        {
            var contextOptions = new DbContextOptionsBuilder<SearchContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SearchContext(contextOptions);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var doc1 = new Doc() {Name = "123", Content = "hello to my dear child"};
            doc1.WordsOfDoc.Add(new Word() {Term = "hello"});
            doc1.WordsOfDoc.Add(new Word() {Term = "to"});
            doc1.WordsOfDoc.Add(new Word() {Term = "my"});
            doc1.WordsOfDoc.Add(new Word() {Term = "dear"});
            doc1.WordsOfDoc.Add(new Word() {Term = "child"});

            var doc2 = new Doc() {Name = "456", Content = "hello lazy child"};
            doc2.WordsOfDoc.Add(new Word() {Term = "lazy"});
            doc2.WordsOfDoc.Add(new Word() {Term = "hello"});
            doc2.WordsOfDoc.Add(new Word() {Term = "child"});

            context.Docs.AddRange(doc1, doc2);
            context.SaveChanges();
            return context;
        }
    }
}