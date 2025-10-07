using System;
using System.Collections.Generic;
using System.Linq;

interface IIdification
{
    string Id { get; }
}

class Citizen : IIdification
{
    public string Name { get;  }
    public int Age { get; }
    public string Id { get; }

    public Citizen(string name, int age, string id)
    {
        Name = name;
        Age = age;
        Id = id;
    }
}

class Robot : IIdification
{
    public string Model { get; }
    public string Id { get; }

    public Robot(string model, string id)
    {
        Model = model;
        Id = id;
    }
}

class Program
{
    static void Main()
    {
        var identificators = new List<IIdification>();
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            string[] parts = input.Split();
            if (parts.Length == 3)
            {
                Citizen citizen = new Citizen(parts[0], int.Parse(parts[1]), parts[2]);
                identificators.Add(citizen);
            }
            else if (parts.Length == 2)
            {
                Robot robot = new Robot(parts[0], parts[1]);
                identificators.Add(robot);
            }
        }
        string fakeId = Console.ReadLine();
        foreach (var identificator in identificators.Where(i => i.Id.EndsWith(fakeId)))
        {
            Console.WriteLine(identificator.Id);
        }
    }
}