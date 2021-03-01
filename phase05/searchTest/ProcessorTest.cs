using System.Collections.Generic;
using NSubstitute;
using search;
using Xunit;

namespace searchTest
{
    public class ProcessorTest
    {
        private readonly ISearch _sut;
        private readonly IInvertedIndex _hashInvertedIndex;

        public ProcessorTest()
        {
            _hashInvertedIndex = Substitute.For<IInvertedIndex>();
            _sut = new Processor(_hashInvertedIndex);
        }

        public static readonly IEnumerable<object[]> InputToSearch = new List<object[]>
        {
            new object[]
            {
                new HashSet<string>() {"dad", "hello"},
                new HashSet<string>() {"car", "bank"},
                new HashSet<string>() {"evil"},
                new HashSet<string>(){"1"}
            },
            new object[]
            {
                new HashSet<string>() {"dad", "hello"},
                new HashSet<string>(),
                new HashSet<string>() {"evil"},
                new HashSet<string>(){"1"}
            },
            new object[]
            {
                new HashSet<string>() {"hello"},
                new HashSet<string>() {"car", "bank"},
                new HashSet<string>() {"evil", "car"},
                new HashSet<string>()
            },
            new object[]
            {
                new HashSet<string>() {"hello", "car"},
                new HashSet<string>() {"dad"},
                new HashSet<string>(),
                new HashSet<string>(){"1"}
            },
            new object[]
            {
                new HashSet<string>() {"hello"},
                new HashSet<string>() {"car"},
                new HashSet<string>() {"evil"},
                new HashSet<string>(){"1", "2"}
            }

        };
        
        [Theory]
        [MemberData(nameof(InputToSearch))]
        public void Search_ShouldReturnCorrectHashSet(HashSet<string> noSignWords, HashSet<string> plusSignWords,
            HashSet<string> minusSignWords, HashSet<string> expected)
        {
            //Arrange
            _hashInvertedIndex.GetDocsContain("hello").Returns(new HashSet<string>() {"1", "2", "3"});
            _hashInvertedIndex.GetDocsContain("dad").Returns(new HashSet<string>() {"1", "3", "5"});
            _hashInvertedIndex.GetDocsContain("bank").Returns(new HashSet<string>());
            _hashInvertedIndex.GetDocsContain("car").Returns(new HashSet<string>() {"1", "2", "6"});
            _hashInvertedIndex.GetDocsContain("evil").Returns(new HashSet<string>() {"3", "8", "4"});
            
            //Act
            var result = _sut.Search(new DocContainer()
                {NoSignWords = noSignWords, PlusSignWords = plusSignWords, MinusSignWords = minusSignWords});
            
            //Assert
            Assert.Equal(expected, result);
        }
    }
}