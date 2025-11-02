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
    public King(string name)
    {
        if (NameRegistry.usedNames.Contains(name))
            throw new ArgumentException($"{name} is already used!");

        Name = name;
        NameRegistry.usedNames.Add(name);
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
    public int HitCount { get; private set; } = 0;
    public bool IsAlive { get; private set; } = true;
    public event Action<Footman>? Died;

    public Footman(string name)
    {
        if (NameRegistry.usedNames.Contains(name))
            throw new ArgumentException($"{name} is already used!");
        Name = name;
        NameRegistry.usedNames.Add(name);
    }

    public void OnKingAttacked()
    {
        if (IsAlive)
            Console.WriteLine($"Footman {Name} is panicking!");
    }

    public void TakeHit()
    {
        if (!IsAlive) return;

        HitCount++;
        if (HitCount >= 2)
        {
            IsAlive = false;
            Died?.Invoke(this);
        }
    }
}

public class RoyalGuard
{
    public string Name { get; }
    public int HitCount { get; private set; } = 0;
    public bool IsAlive { get; private set; } = true;
    public event Action<RoyalGuard>? Died;

    public RoyalGuard(string name)
    {
        if (NameRegistry.usedNames.Contains(name))
            throw new ArgumentException($"{name} is already used!");
        Name = name;
        NameRegistry.usedNames.Add(name);
    }

    public void OnKingAttacked()
    {
        if (IsAlive)
            Console.WriteLine($"Royal Guard {Name} is defending!");
    }

    public void TakeHit()
    {
        if (!IsAlive) return;

        HitCount++;
        if (HitCount >= 3)
        {
            IsAlive = false;
            Died?.Invoke(this);
        }
    }
}

class Program
{
    static void Main()
    {
        string kingName = Console.ReadLine();
        string[] guardNames = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] footmanNames = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        var king = new King(kingName);

        var guards = new List<RoyalGuard>();
        foreach (var name in guardNames)
        {
            var guard = new RoyalGuard(name);
            guards.Add(guard);
            king.KingAttacked += guard.OnKingAttacked;

            guard.Died += g =>
            {
                king.KingAttacked -= g.OnKingAttacked;
                guards.Remove(g);
            };
        }

        var footmen = new List<Footman>();
        foreach (var name in footmanNames)
        {
            var footman = new Footman(name);
            footmen.Add(footman);
            king.KingAttacked += footman.OnKingAttacked;

            footman.Died += f =>
            {
                king.KingAttacked -= f.OnKingAttacked;
                footmen.Remove(f);
            };
        }

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            if (command == "Attack King")
            {
                king.Attack();
            }
            else if (command.StartsWith("Kill"))
            {
                string nameToKill = command.Split(' ')[1];

                var guard = guards.Find(g => g.Name == nameToKill);
                if (guard != null)
                {
                    guard.TakeHit();
                    continue;
                }

                var footman = footmen.Find(f => f.Name == nameToKill);
                if (footman != null)
                {
                    footman.TakeHit();
                }
            }
        }
    }
}
