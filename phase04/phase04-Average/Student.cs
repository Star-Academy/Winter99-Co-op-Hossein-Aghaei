using System.Collections.Generic;
using System.Linq;
using System;

namespace phase04_Average
{
    public class Student
    {
        public int StudentNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public Student(int studentNumber, string firstName, string lastName)
        {
            StudentNumber = studentNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   StudentNumber == student.StudentNumber &&
                   FirstName == student.FirstName &&
                   LastName == student.LastName;
        }
    }
}