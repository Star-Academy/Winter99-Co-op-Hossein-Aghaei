using System;
using phase04_Average;

namespace main
{
    class Program
    {
        static void Main(string[] args) {

            var _fileReader = new FileReader("C:/Users/hos3in/Desktop/New folder (2)/Winter99-Co-op-Hossein-Aghaei/phase04/grades.json",
            "files/student.json");
            var _objectCreator = new ObjectCreator();

            var _controller = new Controller(_objectCreator, _fileReader);

            foreach (var student in _controller.GetNamesAndAverages())
            {
                Console.WriteLine(student.NameAndLAstName + " : " + student.Average);
            }
        }
    }
}