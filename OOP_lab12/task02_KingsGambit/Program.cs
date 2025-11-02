using System;
using System.Collections.Generic;

public class NameRegistry
{
    public static HashSet<string> usedNames = new HashSet<string>();
}

public delegate void KingAttackedEventHandler();
public class King
{
    public string Name { get; }
    public King (string name)
    {
        if (NameRegistry.usedNames.Contains (name))
        {
            throw new ArgumentException($"{name} is already used!");
        }
        Name = name;
        NameRegistry.usedNames.Add (name);
    }

    public event KingAttackedEventHandler? KingAttacked;
    
    public void Attack()
    {
        Console.WriteLine($"King {Name} is under attack!");
        KingAttacked?.Invoke();
    }
}

public class Footman
{
    public string Name { get; }
    public bool IsAlive { get; private set; } = true;
    public Footman(string name)
    {
        if (NameRegistry.usedNames.Contains(name))
        {
            throw new ArgumentException($"{name} is already used!");
        }
        Name = name;
        NameRegistry.usedNames.Add(name);
    }

    public void OnKingAttacked()
    {
        if (IsAlive)
        { Console.WriteLine($"Footman {Name} is panicking!"); }
    }

    public void Kill()
    {
        IsAlive = false;
    }
}

public class RoyalGuard
{
    public string Name { get; }
    public bool IsAlive { get; private set; } = true;
    public RoyalGuard(string name)
    {
        if (NameRegistry.usedNames.Contains(name))
        {
            throw new ArgumentException($"{name} is already used!");
        }
        Name = name;
        NameRegistry.usedNames.Add(name);
    }

    public void OnKingAttacked()
    {
        if (IsAlive)
        { Console.WriteLine($"Royal Guard {Name} is defending!"); }
    }
    public void Kill()
    {
        IsAlive = false;
    }
}

class Program
{
    static void Main()
    {
        string kingName = Console.ReadLine();
        string[] guardName = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] footmanName = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var king = new King(kingName);
        var guards = new List<RoyalGuard>();
        foreach(var name in guardName)
        {
            var guard = new RoyalGuard(name);
            guards.Add(guard);
            king.KingAttacked += guard.OnKingAttacked;
        }
        var footmen = new List<Footman>();
        foreach (var name in footmanName)
        {
            var footman = new Footman(name);
            footmen.Add(footman);
            king.KingAttacked += footman.OnKingAttacked;
        }

        string command;
        while((command = Console.ReadLine()) != "End")
        {
            if (command == "Attack King")
            {
                king.Attack();
            }
            else if (command.StartsWith("Kill"))
            {
                string nameToKill = command.Split(' ')[1];

                RoyalGuard guard = guards.Find(g => g.Name == nameToKill);
                if (guard != null)
                {
                    guard.Kill();
                    continue;
                }

                Footman f = footmen.Find(ff => ff.Name == nameToKill);
                if (f != null)
                {
                    f.Kill();
                }
            }
            
        }
    }
}