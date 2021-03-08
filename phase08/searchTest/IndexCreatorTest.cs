using System;
using System.Collections.Generic;
using NSubstitute;
using search;
using Xunit;

namespace searchTest
{
    public class IndexCreatorTest
    {
        private readonly IndexCreator _sut;
        private readonly IFileReader _docFileReader;
        private readonly IDocumentRepository _documentRepository;

        public IndexCreatorTest()
        {
            _docFileReader = Substitute.For<IFileReader>();
            _documentRepository = new DocumentRepositoryForTest();
            _sut = new IndexCreator(_docFileReader, _documentRepository);
        }

        [Fact]
        public void OrganizeDocsAndWords_ShouldAddNewDocWithItsWords()
        {
            var returnedScanData = new Dictionary<string, string>()
            {
                {"57110", "found. hello"}, {"58043", "found+"}, {"58046", "find ++==dad soon!"}
            };
            _docFileReader.ScanData().Returns(returnedScanData);
            
            _sut.OrganizeDocsAndWords();
            var result = _documentRepository.GetDocsContain("found");
            var expected = new HashSet<string>() {"57110", "58043"};

            Assert.Equal(expected, result);

        }
        
    }
}