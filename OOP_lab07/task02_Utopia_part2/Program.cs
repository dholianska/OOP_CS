using System;
using System.Collections.Generic;
using System.Linq;

interface IBirthday
{
    string DateOfBirth { get; }
}

class Citizen : IBirthday
{
    public string Name { get; }
    public int Age { get; }
    public string Id { get; }
    public string DateOfBirth { get; }

    public Citizen(string name, int age, string id, string dateOfBirth)
    {
        Name = name;
        Age = age;
        Id = id;
        DateOfBirth = dateOfBirth;
    }
}
class Robot
{
    public string Model { get; }
    public string Id { get; }
    public Robot(string model, string id)
    {
        Model = model;
        Id = id;
    }
}
class Pet : IBirthday
{
    public string Name { get; }
    public string DateOfBirth { get; }

    public Pet(string name, string dateOfBirth)
    {
        Name = name;
        DateOfBirth = dateOfBirth;
    }
}

class Program
{
    static void Main()
    {
        var birthday = new List<IBirthday>();
        string input;
        while ((input = Console.ReadLine()) != "End")
        {
            string[] parts = input.Split();
            if (parts[0] == "Citizen")
            {
                Citizen citizen = new Citizen(parts[1], int.Parse(parts[2]), parts[3], parts[4]);
                birthday.Add(citizen);
            }
            else if (parts[0] == "Pet")
            {
                Pet pet = new Pet(parts[1], parts[2]);
                birthday.Add(pet);
            }
        }
        string year = Console.ReadLine();
        foreach (var birth in birthday.Where(b => b.DateOfBirth.EndsWith(year)))
        {
            Console.WriteLine(birth.DateOfBirth);
        }
    }
}