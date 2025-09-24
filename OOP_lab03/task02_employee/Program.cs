using System;
using System.Collections.Generic;
using System.Globalization;

/* Pesho 120.00 Dev Development pesho@abv.bg 28
Toncho 333.33 Manager Marketing 33
Ivan 840.20 ProjectLeader Development ivan@ivan.com
Gosho 0.20 Freeloader Nowhere 18
*/
class Program 
{
    class Employee
    {
        public string Name;
        public double Salary;
        public string Position;
        public string Department;
        public string Email;
        public int Age;

        public Employee(string name, double salary, string position, string department, string email = "n/a", int age = -1)
        {
            Name = name;
            Salary = salary;
            Position = position;
            Department = department;
            Email = email;
            Age = age;
        }
    }

    static void Main()
    {
        Console.WriteLine("enter amount of employers: ");
        int n = int.Parse(Console.ReadLine());
        List<Employee> employees = new List<Employee>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string name = input[0];
            double salary = double.Parse(input[1], CultureInfo.InvariantCulture);
            string position = input[2];
            string department = input[3];

            string email = "n/a";
            int age = -1;

            for (int j = 4; j < input.Length; j++)
            {
                if (input[j].Contains("@")) email = input[j];
                else age = int.Parse(input[j]);
            }

            employees.Add(new Employee(name, salary, position, department, email, age));
        }

        string bestDept = "";
        double highestAvg = 0;

        foreach (var emp in employees)
        {
            string dept = emp.Department;

            bool alreadyChecked = bestDept != "" && dept == bestDept;
            if (alreadyChecked) continue;

            double sum = 0;
            int count = 0;
            foreach (var e in employees)
            {
                if (e.Department == dept)
                {
                    sum += e.Salary;
                    count++;
                }
            }

            double avg = sum / count;
            if (avg > highestAvg)
            {
                highestAvg = avg;
                bestDept = dept;
            }
        }

        Console.WriteLine("highest average salary:");
        Console.WriteLine(bestDept);

        List<Employee> bestEmployees = new List<Employee>();
        foreach (var emp in employees)
        {
            if (emp.Department == bestDept)
                bestEmployees.Add(emp);
        }

        bestEmployees.Sort((a, b) => b.Salary.CompareTo(a.Salary));
        

        foreach (var emp in bestEmployees)
        {
            Console.WriteLine($"{emp.Name} {emp.Salary:F2} {emp.Email} {emp.Age}");
        }
    }
}
