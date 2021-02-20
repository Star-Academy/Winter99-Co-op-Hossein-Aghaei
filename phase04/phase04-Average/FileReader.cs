using System.IO;

namespace phase04_Average
{
    public class FileReader : IFileReader
    {
        private string _gradesPath;
        private string _studentsPath;

        public FileReader(string gradePath, string studentsPath)
        {
            this._gradesPath = gradePath;
            this._studentsPath = studentsPath;
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