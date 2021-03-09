using System.Collections.Generic;

namespace search
{
    public interface IFileReader
    {
        Dictionary<string, string> ScanData(string path);
    }
}