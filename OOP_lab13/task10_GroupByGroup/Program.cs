using System;
using System.Linq;
using System.Collections.Generic;

class Person
{
    public string Name { get; set; }
    public int Group {  get; set; }
    
    public Person (string name, int group)
    {
        Name = name;
        Group = group;
    }
}

class Program
{
    static void Main()
    {
        var people = new List<Person>();
        string input;
        while ((input = Console.ReadLine()) != "END")
        {
            string[] parts = input.Split(' ');
            string name = parts[0] + " " + parts[1];
            int group = int.Parse(parts[2]);

            var person = new Person(name, group);
            people.Add(person);
        }
        var selectedPeople =
            from p in people
            group p by p.Group into g
            orderby g.Key
            select g;

        foreach (var person in selectedPeople)
        {
            string names = string.Join(", ", person.Select(x => x.Name));
            Console.WriteLine($"{person.Key} - {names}");
        }
    }
}