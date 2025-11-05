using System;
using System.Linq;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Group {  get; set; }

    public Student(string firstName, string lastName, int group)
    {
        FirstName = firstName; 
        LastName = lastName;
        Group = group;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}

class Program
{
    static void Main()
    {
        var students = new List<Student>();
        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] parts = input.Split(' ');
            string firstName = parts[0];
            string lastName = parts[1];
            int group = int.Parse(parts[2]);

            var student = new Student(firstName, lastName, group);
            students.Add(student);
        }

        var selectedStudents = students.Where(g => g.Group == 2).OrderBy(fn => fn.FirstName);

                                            /*  var selectedStudents =
                                                from s in students
                                                where s.Group == 2
                                                orderby s.FirstName
                                                select s; */

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(student);
        }
    }
}