using System.Collections.Generic;
using elasticsearch.model;

namespace elasticsearch.DocManager
{
    public interface IDocFactory
    {
        List<Doc> GetAllDocuments(string directoryPath);
    }
}