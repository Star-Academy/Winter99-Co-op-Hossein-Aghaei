using System;

namespace phase04_Average
{
    public class Grade
    { 
        public int StudentNumber { get;}
        public string Lesson{ get; }
        public double Score{ get; }
    
        public Grade(int StudentNumber, string Lesson, double Score){ 
            this.StudentNumber = StudentNumber;
            this.Lesson = Lesson;
            this.Score = Score;
        }

        public override bool Equals(object obj)
        {
            return obj is Grade grade &&
                   StudentNumber == grade.StudentNumber &&
                   Lesson == grade.Lesson &&
                   Score == grade.Score;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StudentNumber, Lesson, Score);
        }
    }
}