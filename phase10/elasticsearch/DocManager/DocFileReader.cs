using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace elasticsearch.DocManager
{
    public class DocFileReader : IFileReader
    {
        public Dictionary<string, string> ScanData(string directoryPath)
        {
            var docsWithWords = GetAllFilesOfDirectory(directoryPath).Select(x => new
            {
                FileName = GetFileName(x),
                FileContent = ReadDocContent(x)
            });
            return docsWithWords.ToDictionary(x => x.FileName, x => x.FileContent);
        }

        private static string GetFileName(string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        private static string ReadDocContent(string filePath)
        {
            return File.ReadAllText(filePath).ToLower();
        }

        private static IEnumerable<string> GetAllFilesOfDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath)) throw new IOException("Directory doesn't exists!");
            var allFiles = Directory.GetFiles(directoryPath);
            return allFiles;
        }
    }
}