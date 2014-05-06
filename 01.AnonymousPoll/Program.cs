using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.AnonymousPoll
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\students");
            List<Student> students = new List<Student>();

            foreach (string line in lines)
            {
                string[] student = line.Split(',');
                students.Add(new Student(student[0], student[1], student[2], student[3], student[4]));
            }

            int numCases = int.Parse(Console.ReadLine());
            string[] outputs = new string[numCases];

            for (int i = 0; i<numCases; i++)
            {
                string[] data = Console.ReadLine().Split(',');

                var matches = students.Where(x => x.Gender == data[0] && x.Age == data[1] && x.Studies == data[2] && x.AcademicYear == data[3]).OrderBy(x => x.Name).ToList();

                string result;

                if (matches.Count == 0)
                    result = "NONE";
                else
                    result = String.Join(",", matches.Select(x => x.Name).ToList());

                outputs[i] = String.Format("Case #{0}: {1}", i+1, result);
            }

            foreach (var o in outputs)
            {
                Console.WriteLine(o);
            }

        }

        private class Student
        {
            public string Name {get; set;}
            public string Age {get; set;}
            public string Gender {get; set;}
            public string Studies {get; set;}
            public string AcademicYear {get; set;}
            
            public Student(string name, string gender, string age, string studies, string academicYear)
            {
                Name = name;
                Age = age;
                Gender = gender;
                Studies = studies;
                AcademicYear = academicYear;
            }
        }
    }
}
