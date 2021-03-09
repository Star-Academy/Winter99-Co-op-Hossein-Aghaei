using System.Collections.Generic;
using NSubstitute;
using search;
using Xunit;

namespace searchTest
{
    public class ProcessorTest
    {
        private readonly ISearch _sut;
        private readonly IDocumentRepository _documentRepository;

        public ProcessorTest()
        {
            _documentRepository = Substitute.For<IDocumentRepository>();
            _sut = new Processor(_documentRepository);
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
            _documentRepository.GetDocsContain("hello").Returns(new HashSet<string>() {"1", "2", "3"});
            _documentRepository.GetDocsContain("dad").Returns(new HashSet<string>() {"1", "3", "5"});
            _documentRepository.GetDocsContain("bank").Returns(new HashSet<string>());
            _documentRepository.GetDocsContain("car").Returns(new HashSet<string>() {"1", "2", "6"});
            _documentRepository.GetDocsContain("evil").Returns(new HashSet<string>() {"3", "8", "4"});
            
            var result = _sut.Search(new DocContainer()
                {NoSignWords = noSignWords, PlusSignWords = plusSignWords, MinusSignWords = minusSignWords});
            
            Assert.Equal(expected, result);
        }
    }
}