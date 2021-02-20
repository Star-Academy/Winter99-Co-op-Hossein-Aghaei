using System.Collections.Generic;
using System.Linq;
using System;

namespace phase04_Average
{
    public class Controller
    {
        private IObjectCreator _objectCreator;
        private IFileReader _fileReader;
        public Controller(IObjectCreator objectCreator, IFileReader fileReader)
        {
            this._objectCreator = objectCreator;
            this._fileReader = fileReader;
        }

        public IEnumerable<StudentInfo> GetNamesAndAverages(){
            List<Student> students = this._objectCreator.GetAllStudents(this._fileReader.ReadStudentsJson());
            List<Grade> grades = this._objectCreator.GetAllGrades(this._fileReader.ReadGradesJson());
            var result = students.GroupJoin(grades, s => s.StudentNumber, g => g.StudentNumber,
             (student, grade) => new StudentInfo{ NameAndLAstName = student.FirstName + " " + student.LastName, Average = grade.Average(a => a.Score)}).Take(3);
            return result;
        }
    }

    public class StudentInfo
     {
        public string NameAndLAstName{ get; set; }
        public double Average{ get; set; }

        public override bool Equals(object obj)
        {
            return obj is StudentInfo info &&
                   NameAndLAstName == info.NameAndLAstName &&
                   Average == info.Average;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NameAndLAstName, Average);
        }
    }
}