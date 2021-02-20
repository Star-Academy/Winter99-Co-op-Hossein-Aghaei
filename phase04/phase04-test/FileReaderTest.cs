using System;
using Xunit;
using phase04_Average;

namespace phase04_test
{
    public class FileReaderTest
    {
        [Fact]
        public void ShouldReadStudentsJson(){
             string studentsJsonPath = @"C:\Users\hos3in\Desktop\New folder (2)\Winter99-Co-op-Hossein-Aghaei\phase04\phase04-test\sample-file-test\student.json";
             var fileReader = new FileReader("", studentsJsonPath);
             string expected = @"[{
        ""StudentNumber"": 1,
        ""FirstName"": ""Mahdi"",
        ""LastName"": ""Malverdi""
    },
    {
        ""StudentNumber"": 2,
        ""FirstName"": ""Mohammad"",
        ""LastName"": ""Haghighat""
    },
    {
        ""StudentNumber"": 3,
        ""FirstName"": ""Mohammad Hossein"",
        ""LastName"": ""Mostmand""
    }
]";
            Assert.Equal(expected, fileReader.ReadStudentsJson());
        }

        [Fact]
        public void ShouldRedGradesJson() {
            string gradesJsonPath = @"C:\Users\hos3in\Desktop\New folder (2)\Winter99-Co-op-Hossein-Aghaei\phase04\phase04-test\sample-file-test\grade.json";
            var fileReader = new FileReader(gradesJsonPath, "");
            string expected = @"[{
    ""StudentNumber"": 1,
    ""Lesson"": ""DB"",
    ""Score"": 14.63433486
}]";
            Assert.Equal(expected, fileReader.ReadGradesJson());
        }
    }
}
