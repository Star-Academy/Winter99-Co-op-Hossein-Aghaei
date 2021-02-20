using System.Collections.Generic;
using Newtonsoft.Json;

namespace phase04_Average
{
    public class ObjectCreator : IObjectCreator
    {
        public List<Student> GetAllStudents(string studentsJson)
        {
            return JsonConvert.DeserializeObject<List<Student>>(studentsJson);
        }

        public List<Grade> GetAllGrades(string gradesJson)
        {
            return JsonConvert.DeserializeObject<List<Grade>>(gradesJson);
        }

    }
}