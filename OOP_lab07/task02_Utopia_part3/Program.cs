using System;
using System.Collections.Generic;
using System.Linq;
class NameRegistry
{
    public static HashSet<string> usedNames = new HashSet<string>();
}
interface IBuyer
{
    int Food { get; }
    int BuyFood();
}
class Citizen : IBuyer
{
    public string Name { get; }
    public int Age { get; }
    public string Id { get; }
    public string DateOfBirth { get; }
    public int Food { get; set; }
    public Citizen(string name, int age, string id, string dateOfBirth)
    {
        if (NameRegistry.usedNames.Contains(name))
            throw new ArgumentException($"name {name} is already used!");
        Name = name;
        Age = age;
        Id = id;
        DateOfBirth = dateOfBirth;
        Food = 0;

        NameRegistry.usedNames.Add(name);
    }
    public int BuyFood()
    {
        Food += 10;
        return Food;
    }
}
class Rebel : IBuyer
{
    public string Name { get; }
    public int Age { get; }
    public string Group { get; }
    public int Food { get; set; }
    public Rebel(string name, int age, string group)
    {
        if (NameRegistry.usedNames.Contains(name))
            throw new ArgumentException($"name {name} is already used!");
        Name = name;
        Age = age;
        Group = group;
        Food = 0;

        NameRegistry.usedNames.Add(name);
    }
    public int BuyFood()
    {
        Food += 5;
        return Food;
    }
}
class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var buyers = new List<IBuyer>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();

            if (input.Length == 4)
            {
                buyers.Add(new Citizen(input[0], int.Parse(input[1]), input[2], input[3]));
            }
            else if (input.Length == 3)
            {
                buyers.Add(new Rebel(input[0], int.Parse(input[1]), input[2]));
            }
        }

        string command = Console.ReadLine();
        while (command != "End")
        {
            foreach (var buyer in buyers)
            {
                if ((buyer is Citizen c && c.Name == command) ||
                    (buyer is Rebel r && r.Name == command))
                {
                    buyer.BuyFood();
                    break;
                }
            }

            command = Console.ReadLine();
        }

        Console.WriteLine(buyers.Sum(b => b.Food));
    }
}