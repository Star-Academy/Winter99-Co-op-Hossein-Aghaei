using System.Collections.Generic;

namespace phase04_Average
{
    public interface IObjectCreator
    {
         List<Grade> GetAllGrades(string gradesJson);
        List<Student> GetAllStudents(string studentsJson);
    }
}