using System;
using System.Collections.Generic;
using search;
using NSubstitute;
using Xunit;

namespace searchTest
{
    public class IndexCreatorTests
    {
        private readonly IndexCreator _sut;
        private readonly IFileReader _docFileReader;

        public IndexCreatorTests()
        {
            _docFileReader = Substitute.For<IFileReader>();
            _sut = new IndexCreator(_docFileReader);
        }

        [Fact]
        public void OrganizeDocsAndWords_ShouldReturnCorrectDictionary()
        {
            //Arrange
            var doc1 = new HashSet<string>()
            {
                "57110", "58043"
            };
            var doc2 = new HashSet<string>()
            {
                "57110"
            };
            var expected = new Dictionary<string, HashSet<string>>()
            {
                {"found", doc1}, {"hello", doc2}
            };
            var returnedScanData = new Dictionary<string, string>()
            {
                {"57110", "found. hello"}, {"58043", "found+"}
            };
            _docFileReader.ScanData().Returns(returnedScanData);
            
            //Act
            var result = _sut.OrganizeDocsAndWords();
            
            //Assert
            Assert.Equal(expected, result);
        }
    }
}