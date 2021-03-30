using System.Collections.Generic;

namespace elasticsearch.DocManager
{
    public interface IFileReader
    {
        Dictionary<string, string> ScanData(string path);
    }
}