using System;
using System.Linq;
using System.Collections.Generic;

public interface IFaculty
{
    string FacNum { get; }
}
class StudentSpecialty : IFaculty
{
    public string SpecialtyName { get; }
    public string FacNum { get; }

    public StudentSpecialty(string speicaltyName, string facNum)
    {
        SpecialtyName = speicaltyName;
        FacNum = facNum;
    }

}

class Student : IFaculty
{
    public string Name { get; }
    public string FacNum { get; }

    public Student(string name, string facNum)
    {
        Name = name;
        FacNum = facNum;
    }
}

class Program
{
    static void Main()
    {
        string input1;
        var specialties = new List<StudentSpecialty>();
        var students = new List<Student>();

        while ((input1 = Console.ReadLine()) != "Students:")
        {
            string[] parts = input1.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string specialtyName = string.Join(" ", parts.Take(parts.Length - 1));
            string facNum = parts.Last();

            var specialty = new StudentSpecialty(specialtyName, facNum);
            specialties.Add(specialty);
        }

        string input2;
        while ((input2 = Console.ReadLine()) != "END")
        {
            string[] parts = input2.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string facNum = parts[0];
            string name = string.Join(" ", parts.Skip(1));

            var student = new Student(name, facNum);
            students.Add(student);
        }

        var joinedList = specialties.Join(
            students,
            specialty => specialty.FacNum,
            student => student.FacNum,
            (specialty, student) => new
            {
                SpecialtyName = specialty.SpecialtyName,
                StudentName = student.Name,
                FacultyNumber = student.FacNum
            }).OrderBy(s => s.StudentName);

        foreach (var student in joinedList)
        {
            Console.WriteLine($"{student.StudentName} {student.FacultyNumber} {student.SpecialtyName}");
        }
    }
}