using System.Collections.Generic;
using System;
using Xunit;
using Moq;
using phase04_Average;

namespace phase04_test
{
    public class ControllerTest
    {
        [Fact]
        public void ShouldReturnNamesAndAverages()
        {
            var objectCreator = new Mock<IObjectCreator>();
            var fileReader = new Mock<IFileReader>();
            objectCreator.Setup(x => x.GetAllGrades(It.IsAny<string>())).Returns(GetSomeGGrades());
            objectCreator.Setup(x => x.GetAllStudents(It.IsAny<string>())).Returns(GetSomeStudents());
            var controller = new Controller(objectCreator.Object, fileReader.Object);
            var student1 = new StudentInfo() { NameAndLAstName = "Mahdi Malverdi", Average = 18.5 };
            var student2 = new StudentInfo() { NameAndLAstName = "Mohammad Haghighat", Average = 19.5 };
            var student3 = new StudentInfo() { NameAndLAstName = "Hossein Aghaei", Average = 17 };
            IEnumerable<StudentInfo> result = new List<StudentInfo>(){ student1, student2, student3 };
            Assert.Equal(result, controller.GetNamesAndAverages());
        }

        private List<Grade> GetSomeGGrades(){
            var grade1 = new Grade(1, "DS", 18.5);
            var grade2 = new Grade(2, "DS", 19);
            var grade3 = new Grade(3, "DS", 19);
            var grade4 = new Grade(3, "DB", 15);
            var grade5 = new Grade(2, "DB", 20);
            List<Grade> allGrades = new List<Grade>(){grade1, grade2, grade3, grade4, grade5};
            return allGrades;
        }

        private List<Student> GetSomeStudents(){
            var student1 = new Student(1, "Mahdi", "Malverdi");
            var student2 = new Student(2, "Mohammad", "Haghighat");
            var student3 = new Student(3, "Hossein", "Aghaei");
            List<Student> students = new List<Student>() { student1, student2, student3 };
            return students;
        }
    }
}