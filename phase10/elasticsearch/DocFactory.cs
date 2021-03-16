using System.Collections.Generic;
using System.Linq;
using elasticsearch.model;

namespace elasticsearch
{
    public class DocFactory : IDocFactory
    {
        private readonly IFileReader _fileReader;

        public DocFactory(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IEnumerable<Doc> GetAllDocuments(string directoryPath)
        {
            var allDocuments = _fileReader.ScanData(directoryPath);
            return allDocuments.
                Select(doc => 
                    new Doc()
                    {
                        Name = doc.Key,
                        Content = doc.Value
                    });
        }
    }
}