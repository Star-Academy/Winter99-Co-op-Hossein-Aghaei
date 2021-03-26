using System.Collections.Generic;

namespace elasticsearch
{
    public interface IFileReader
    {
        Dictionary<string, string> ScanData(string path);
    }
}