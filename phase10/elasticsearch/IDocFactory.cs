using System.Collections.Generic;
using elasticsearch.model;

namespace elasticsearch
{
    public interface IDocFactory
    {
        IEnumerable<Doc> GetAllDocuments(string directoryPath);
    }
}