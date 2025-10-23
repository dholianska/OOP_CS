using System;
using System.Collections.Generic;

class Person : IComparable<Person>
{
    private string name;
    private int age;
    private string address;

    public Person (string name, int age, string address)
    {
        Name = name;
        Age = age;
        Address = address;
    }

    public string Name
    {
        get { return name; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("name cannot be null");
            }
            name = value;
        }
    }
    public int Age
    {
        get { return age; }
        set
        {
            if (value <=0)
            {
                throw new ArgumentNullException("age cannot be null");
            }
            age = value;
        }
    }
    public string Address
    {
        get { return address; }
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("address cannot be null");
            }
            address = value;
        }
    }
    public int CompareTo(Person other)
    {
        if (other == null) return 1;

        int nameResult = Name.CompareTo(other.Name);
        if (nameResult != 0)
            return nameResult;

        int ageResult = Age.CompareTo(other.Age);
        if (ageResult != 0)
            return ageResult;

        return Address.CompareTo(other.Address);
    }

}

class Program
{
    static void Main()
    {
        List<Person> people = new List<Person>();

        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string name = parts[0];
            int age = int.Parse(parts[1]);
            string city = parts[2];
            people.Add(new Person(name, age, city));
        }

        int index = int.Parse(Console.ReadLine()) - 1;
        Person personToCompare = people[index];

        int equalCount = 0;
        int notEqualCount = 0;

        foreach (var p in people)
        {
            if (p.CompareTo(personToCompare) == 0)
                equalCount++;
            else
                notEqualCount++;
        }

        if (equalCount == 1)
            Console.WriteLine("no matches");
        else
            Console.WriteLine($"{equalCount} {notEqualCount} {people.Count}");
    }
}