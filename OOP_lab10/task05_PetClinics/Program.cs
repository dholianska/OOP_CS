using System;
using System.Collections.Generic;

public class Pet
{
    public string Name { get; }
    public int Age { get; }
    public string Kind { get; }

    public Pet(string name, int age, string kind)
    {
        Name = name;
        Age = age;
        Kind = kind;
    }

    public override string ToString()
    {
        return $"{Name} {Age} {Kind}";
    }
}

public class Clinic
{
    private Pet[] rooms;
    public string Name { get; }

    public Clinic(string name, int roomCount)
    {
        if (roomCount % 2 == 0)
            throw new InvalidOperationException("invalid operation!");

        Name = name;
        rooms = new Pet[roomCount];
    }

    public bool Add(Pet pet)
    {
        int center = rooms.Length / 2;
        for (int i = 0; i < rooms.Length; i++)
        {
            int index = center - i;
            if (index >= 0 && rooms[index] == null)
            {
                rooms[index] = pet;
                return true;
            }

            index = center + i;
            if (index < rooms.Length && rooms[index] == null)
            {
                rooms[index] = pet;
                return true;
            }
        }
        return false;
    }
    public bool Release()
    {
        bool released = false;

        for (int i = 0; i < rooms.Length; i++)
        {
            if (rooms[i] != null)
            {
                rooms[i] = null;
                released = true;
            }
        }

        return released;
    }

    public bool HasEmptyRooms()
    {
        foreach (var room in rooms)
        {
            if (room == null)
                return true;
        }
        return false;
    }

    public void Print(int roomNumber)
    {
        var pet = rooms[roomNumber - 1];
        Console.WriteLine(pet == null ? "Room empty" : pet.ToString());
    }

    public void PrintAll()
    {
        foreach (var pet in rooms)
        {
            Console.WriteLine(pet == null ? "Room empty" : pet.ToString());
        }
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        var pets = new Dictionary<string, Pet>();
        var clinics = new Dictionary<string, Clinic>();

        for (int i = 0; i < n; i++)
        {
            string[] parts = Console.ReadLine().Split();
            string command = parts[0];

            try
            {
                switch (command)
                {
                    case "Create":
                        if (parts[1] == "Pet")
                        {
                            string name = parts[2];
                            int age = int.Parse(parts[3]);
                            string kind = parts[4];
                            pets[name] = new Pet(name, age, kind);
                        }
                        else if (parts[1] == "Clinic")
                        {
                            string name = parts[2];
                            int rooms = int.Parse(parts[3]);
                            clinics[name] = new Clinic(name, rooms);
                        }
                        break;

                    case "Add":
                        string petName = parts[1];
                        string clinicName = parts[2];
                        if (!pets.ContainsKey(petName) || !clinics.ContainsKey(clinicName))
                            throw new InvalidOperationException("Invalid Operation!");

                        Console.WriteLine(clinics[clinicName].Add(pets[petName]));
                        break;

                    case "Release":
                        Console.WriteLine(clinics[parts[1]].Release());
                        break;

                    case "HasEmptyRooms":
                        Console.WriteLine(clinics[parts[1]].HasEmptyRooms());
                        break;

                    case "Print":
                        string cName = parts[1];
                        if (parts.Length == 2)
                            clinics[cName].PrintAll();
                        else
                        {
                            int roomNumber = int.Parse(parts[2]);
                            clinics[cName].Print(roomNumber);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}