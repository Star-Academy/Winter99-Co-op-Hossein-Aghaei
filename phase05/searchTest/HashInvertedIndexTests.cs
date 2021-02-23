using System.Collections.Generic;
using search;
using Xunit;
using FluentAssertions;

namespace searchTest
{
    public class HashInvertedIndexTests
    {
        private HashInvertedIndex _sut;

        
        [Fact]
        public void TryGetDocsContainWord_ShouldSetCorrectDocs_WhenInputExists()
        {
            //Arrange
            var docs = new HashSet<string>()
            {
                "57110"
            };
            var words = new Dictionary<string, HashSet<string>>()
            {
                {"found", docs}
            };
            _sut = new HashInvertedIndex(words);

            //Act
            var result = _sut.TryGetDocsContain("found",  result: out var expected);
            
            //Assert
            Assert.Equal(expected, docs);
        }

        [Fact]
        public void GetDocsContain_ShouldReturnFalse_WhenInputDoesNotExists()
        {
            //Arrange
            var docs = new HashSet<string>()
            {
                "57110"
            };
            var words = new Dictionary<string, HashSet<string>>()
            {
                {"found", docs}
            };
            _sut = new HashInvertedIndex(words);
            
            //Act
            var result = _sut.TryGetDocsContain("go", out var expected);
            
            //Assert
            result.Should().BeFalse();
        }
    }
}