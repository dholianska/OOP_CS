using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    private string _enrollCode;
    public string EnrollCode
    {
        get { return _enrollCode; }
        set
        {
            if (value.Length != 6)
            {
                throw new ArgumentException("faculty number has to have 6 digits");
            }
            _enrollCode = value;
        }
    }
    public List<int> Grades { get; set; }

    public Student(string enrollCode, List<int> grades)
    {
        EnrollCode = enrollCode;
        Grades = grades;
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
            string enrollCode = parts[0];
            var grades = parts.Skip(1).Select(int.Parse).ToList();

            var student = new Student(enrollCode, grades);
            students.Add(student);
        }

        var selectedStudents = students.Where(s => s.EnrollCode.EndsWith("14") || s.EnrollCode.EndsWith("15"));

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(string.Join(" ", student.Grades));
        }
    }
}