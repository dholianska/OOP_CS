using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<int> Grades { get; set; }

    public Student(string firstName, string lastName, List<int> grades)
    {
        FirstName = firstName;
        LastName = lastName;
        Grades = grades;
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
            var grades = parts.Skip(2).Select(int.Parse).ToList();

            var student = new Student(firstName, lastName, grades);
            students.Add(student);
        }

        var selectedStudents = students.Where(s => s.Grades.Contains(6));

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(student);
        }
    }
}