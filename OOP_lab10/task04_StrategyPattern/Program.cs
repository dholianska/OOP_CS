using System;
using System.Collections.Generic;
using System.Reflection;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString()
    {
        return $"{Name} {Age}";
    }
}

public class NameComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        if (x == null || y == null) return 0;

        int lengthCompare = x.Name.Length.CompareTo(y.Name.Length);
        if (lengthCompare != 0)
            return lengthCompare;

        char firstX = char.ToLower(x.Name[0]);
        char firstY = char.ToLower(y.Name[0]);
        return firstX.CompareTo(firstY);
    }
}

public class AgeComparer : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        if (x == null || y == null) return 0;
        return x.Age.CompareTo(y.Age);
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var nameSet = new SortedSet<Person>(new NameComparer());
        var ageSet = new SortedSet<Person>(new AgeComparer());

        for (int  i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');
            string name = parts[0];
            int age = int.Parse(parts[1]);

            var person = new Person(name, age);
            nameSet.Add(person);
            ageSet.Add(person);
        }

        foreach (var name in nameSet)
        {
            Console.WriteLine(name);
        }
        foreach (var age in ageSet)
        {
            Console.WriteLine(age);
        }
    }
}