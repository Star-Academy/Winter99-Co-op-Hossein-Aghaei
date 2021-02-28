using System.IO;

namespace phase04_Average
{
    public class FileReader : IFileReader
    {
        private readonly string _gradesPath;
        private readonly string _studentsPath;

        public FileReader(string gradePath, string studentsPath)
        {
            _gradesPath = gradePath;
            _studentsPath = studentsPath;
        }

        public string ReadGradesJson()
        {
            return File.ReadAllText(_gradesPath);
        }

        public string ReadStudentsJson()
        {
            return File.ReadAllText(_studentsPath);
        }

    }
}