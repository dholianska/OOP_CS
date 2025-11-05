using System;
using System.Linq;

class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public Student(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Age}";
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
            int age = int.Parse(parts[2]);

            var student = new Student(firstName, lastName, age);
            students.Add(student);
        }

        var selectedStudents = students.Where(s => s.Age>=18 && s.Age<=24);

                                              /* var selectedStudents =
                                                from s in students
                                                where s.Age >= 18 && s.Age <= 24
                                                select s;
                                              */

        foreach (var student in selectedStudents)
        {
            Console.WriteLine(student);
        }
    }
}