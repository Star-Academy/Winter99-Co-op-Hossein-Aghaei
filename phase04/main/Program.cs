using System;
using phase04_Average;

namespace main
{
    internal static class Program
    {
        private static void Main(string[] args) {

            var fileReader = new FileReader(@"files\grades.json", @"files\student.json");
            var objectCreator = new ObjectCreator();
            var controller = new Controller(objectCreator, fileReader);

            foreach (var student in controller.GetNamesAndAverages())
            {
                Console.WriteLine(student.FullName + " : " + student.Average);
                
            }
        }
    }
}
