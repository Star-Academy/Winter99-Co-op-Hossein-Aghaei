using System.Collections.Generic;
using elasticsearch;
using elasticsearch.model;
using Moq;
using NSubstitute;
using Xunit;

namespace elasticsearchTest
{
    public class DocFactoryTest
    {
        private readonly DocFactory _sut;
        private readonly IFileReader _fileReader;
        
        public DocFactoryTest()
        {
            _fileReader = Substitute.For<IFileReader>();
            _sut = new DocFactory(_fileReader);
        }

        [Fact]
        public void GetAllDocuments_ShouldReturnSomeDocs_WhenPathIsOk()
        {
            var returnedScanData = new Dictionary<string, string>()
            {
                {"57110", "found. hello"}, {"58043", "found+"}
            };
            _fileReader.ScanData(It.IsAny<string>()).Returns(returnedScanData);

            var expected = new List<Doc>()
            {
                new Doc()
                {
                    Name = "57110",
                    Content = "found. hello"
                },
                new Doc()
                {
                    Name = "58043",
                    Content = "found+"
                }
            };

            var result = _sut.GetAllDocuments(It.IsAny<string>());
            Assert.Equal(expected, result);
        }
    }
}