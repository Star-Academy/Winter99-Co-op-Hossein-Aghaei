using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace search
{
    public class DocFileReader : IFileReader
    {
        private readonly string _path;

        public DocFileReader(string path)
        {
            _path = path;
        }

        public Dictionary<string, string> ScanData()
        {
            var docsWithWords = GetAllFilesOfDirectory().Select(x => new
            {
                FileName = Path.GetFileNameWithoutExtension(x),
                FileContent = File.ReadAllText(x).ToLower()
            });
            return docsWithWords.ToDictionary(x => x.FileName, x => x.FileContent);
        }

        private IEnumerable<string> GetAllFilesOfDirectory()
        {
            if (!Directory.Exists(_path)) throw new IOException("Directory doesn't exists!");
            var allFiles = Directory.GetFiles(_path);
            return allFiles;
        }
    }
}