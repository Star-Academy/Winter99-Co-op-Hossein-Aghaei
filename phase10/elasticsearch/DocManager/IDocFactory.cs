using System.Collections.Generic;
using elasticsearch.model;

namespace elasticsearch
{
    public interface IDocFactory
    {
        List<Doc> GetAllDocuments(string directoryPath);
    }
}