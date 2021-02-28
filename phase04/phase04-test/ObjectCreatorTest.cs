using Xunit;
using phase04_Average;
using System;
using System.Collections.Generic;

namespace phase04_test
{
    public class ObjectCreatorTest
    {
        [Fact]
        public void ShouldReturnStudens(){
            string studentsJson = @"[{
                ""StudentNumber"": 1,
                ""FirstName"": ""Mahdi"",
                ""LastName"": ""Malverdi""
                }]";
            var student = new Student(1, "Mahdi", "Malverdi");
            List<Student> students = new List<Student>(){student};
            var objectCreator = new ObjectCreator();
            Assert.Equal(students, objectCreator.GetAllStudents(studentsJson));
        }

        [Fact]
        public void ShouldReturnGrades(){
            string gradesJson = @"[{
                ""StudentNumber"": 1,
                ""Lesson"": ""DB"",
                ""Score"": 14.63433486
            }]";
            var grade = new Grade(1, "DB", 14.63433486);
            List<Grade> grades = new List<Grade>(){grade};
            var objectCreator = new ObjectCreator();
            Assert.Equal(grades, objectCreator.GetAllGrades(gradesJson));
        }

        
    }
}