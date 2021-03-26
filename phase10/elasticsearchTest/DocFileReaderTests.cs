using System.Collections.Generic;
using System.IO;
using elasticsearch;
using Xunit;

namespace elasticsearchTest
{
    public class DocFileReaderTests
    {
        private readonly IFileReader _sut = new DocFileReader();

        [Theory]
        [InlineData("sample_data")]
        public void ScanData_ShouldScanDocsAndWords_WhenAddressIsCorrect(string correctPath)
        {
            var expected = new Dictionary<string, string>()
            {
                {
                    "57110",
                    "hello mom"
                },
                {
                    "58043",
                    "i can't do this to you"
                }
            };
            
            var result = _sut.ScanData(Path.GetFullPath(correctPath));
            
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("sample_files")]
        public void scanData_ShouldThrowIOException_WhenAddressIsNotCorrect(string wrongPath)
        {
            void Action() => _sut.ScanData(Path.GetFullPath(wrongPath));

            Assert.Throws<IOException>(Action);
        }
       
    }
}