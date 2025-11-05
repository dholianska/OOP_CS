using System;
using System.Linq;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public Student(string firstName, string lastName, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
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
            string email = parts[2];

            var student = new Student(firstName, lastName, email);
            students.Add(student);
        }

        var selectedStudents = students.Where(s => s.Email.EndsWith("@gmail.com"));

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(student);
        }
    }
}