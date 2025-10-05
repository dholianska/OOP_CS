using System;
using System.Globalization;
using System.Text;

public class Human
{
    private string _name;
    private string _surname;

    public Human(string name, string surname)
    {
        Name = name;
        Surname = surname; 
    }
    
    public string Name
    {
        get { return _name; }
        set
        {
            if (char.IsLower(value[0]))
            {
                throw new ArgumentException("expected upper case letter! argument: firstName");
            }
            if (value.Length <= 3)
            {
                throw new ArgumentException("expected length at least 4 symbols! argument: firstName");
            }
            _name = value;
        }
    }
    public string Surname
    {
        get { return _surname; }
        set
        {
            if (char.IsLower(value[0]))
            {
                throw new ArgumentException("expected upper case letter! argument: lasName");
            }
            if (value.Length < 3)
            {
                throw new ArgumentException("expected length at least 3 symbols! argument: lastName");
            }
            _surname = value;
        }
    }
}

public class Worker : Human
{
    private decimal _weekSalary;
    private int _hoursPerDay;

    public Worker(string name, string surname, decimal weekSalary, int hoursPerDay) : base(name, surname)
    {
        WeekSalary = weekSalary;
        HoursPerDay = hoursPerDay;
    }

    public decimal WeekSalary
    {
        get { return _weekSalary; }
        set
        {
            if (value < 10)
            {
                throw new ArgumentException("expected value dismatch! argument: weekSalary");
            }
            _weekSalary = value;
        }
    }
    public int HoursPerDay
    {
        get { return _hoursPerDay; }
        set
        {
            if (value < 1 || value > 12)
            {
                throw new ArgumentException("expected value dismatch! argument: hoursPerDat");
            }
            _hoursPerDay = value;
        }
    }

    public decimal SalaryPerHour()
    {
        return WeekSalary / (5 * HoursPerDay);
    }

    public override string ToString()
    {
        var resultBuilder = new StringBuilder();
        resultBuilder.AppendLine($"name: {Name}")
            .AppendLine($"surname: {Surname}")
            .AppendLine($"week salary: {WeekSalary:f2}")
            .AppendLine($"hours per day: {HoursPerDay:f2}")
            .AppendLine($"salary per hour: {SalaryPerHour():f2}");
        string result = resultBuilder.ToString().TrimEnd();
        return result;
    }
}

public class Student : Human
{
    private string _facultyNumber;

    public Student(string name, string surname, string facultyNumber) : base(name, surname)
    {
        FacultyNumber = facultyNumber;
    }

    public string FacultyNumber 
    {
        get { return _facultyNumber; }
        set
        {
            if (value.Length < 5 || value.Length > 10)
            {
                throw new ArgumentException("invalid faculty number!");
            }
            _facultyNumber = value;
        }
    }
    public override string ToString()
    {
        var resultBuilder = new StringBuilder();
        resultBuilder.AppendLine($"name: {Name}")
            .AppendLine($"surname: {Surname}")
            .AppendLine($"faculty number: {FacultyNumber}");
        string result = resultBuilder.ToString().TrimEnd();
        return result;
    }
}

class Program
{
    static void Main()
    {
        string[] firstInput = Console.ReadLine().Split();
        string[] secondInput = Console.ReadLine().Split();
        decimal weekSalary = decimal.Parse(secondInput[2], CultureInfo.InvariantCulture);
        int hoursPerDay = int.Parse(secondInput[3], CultureInfo.InvariantCulture);

        try
        {
            var student = new Student(firstInput[0], firstInput[1], firstInput[2]);
            var worker = new Worker(secondInput[0], secondInput[1], weekSalary, hoursPerDay);
            Console.WriteLine();
            Console.WriteLine(student);
            Console.WriteLine();
            Console.WriteLine(worker);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}