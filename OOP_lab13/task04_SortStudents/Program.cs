using System;
using System.Linq;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public Student(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
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

            var student = new Student(firstName, lastName);
            students.Add(student);
        }

        var selectedStudents = students.OrderBy(s => s.LastName).ThenByDescending(s => s.FirstName);

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(student);
        }
    }
}