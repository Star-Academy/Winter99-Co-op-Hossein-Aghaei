using System.Collections.Generic;
using search;
using Xunit;

namespace searchTest
{
    public class HashInvertedIndexTests
    {
        private HashInvertedIndex _sut;
        
        [Fact]
        public void GetDocsContainWord_ShouldSetCorrectDocs_WhenInputExists()
        {
            //Arrange
            var expected = new HashSet<string>()
            {
                "57110"
            };
            var words = new Dictionary<string, HashSet<string>>()
            {
                {"found", expected}
            };
            _sut = new HashInvertedIndex(words);

            //Act
            var result = _sut.GetDocsContain("found");
            
            //Assert
            Assert.Equal(expected, result);
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
            var result = _sut.GetDocsContain("go");
            
            //Assert
            Assert.Equal(new HashSet<string>(), result);
        }
    }
}