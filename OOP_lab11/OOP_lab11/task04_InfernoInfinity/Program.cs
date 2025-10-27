﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace InfernoInfinity
{
    public enum Rarity
    {
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 5
    }

    public enum Clarity
    {
        Chipped = 1,
        Regular = 2,
        Perfect = 5,
        Flawless = 10
    }

    public abstract class Gem
    {
        public int Strength { get; protected set; }
        public int Agility { get; protected set; }
        public int Vitality { get; protected set; }

        protected Gem(int str, int agi, int vit, Clarity clarity)
        {
            Strength = str + (int)clarity;
            Agility = agi + (int)clarity;
            Vitality = vit + (int)clarity;
        }
    }

    public class Ruby : Gem
    {
        public Ruby(Clarity clarity) : base(7, 2, 5, clarity) { }
    }

    public class Emerald : Gem
    {
        public Emerald(Clarity clarity) : base(1, 4, 9, clarity) { }
    }

    public class Amethyst : Gem
    {
        public Amethyst(Clarity clarity) : base(2, 8, 4, clarity) { }
    }

    public abstract class Weapon
    {
        public string Name { get; }
        public int BaseMinDamage { get; }
        public int BaseMaxDamage { get; }
        public Gem[] Sockets { get; }

        protected Weapon(string name, int minDmg, int maxDmg, int sockets, Rarity rarity)
        {
            Name = name;
            BaseMinDamage = minDmg * (int)rarity;
            BaseMaxDamage = maxDmg * (int)rarity;
            Sockets = new Gem[sockets];
        }

        public void AddGem(int index, Gem gem)
        {
            if (index >= 0 && index < Sockets.Length)
                Sockets[index] = gem;
        }

        public void RemoveGem(int index)
        {
            if (index >= 0 && index < Sockets.Length)
                Sockets[index] = null;
        }

        public override string ToString()
        {
            int totalStr = Sockets.Where(g => g != null).Sum(g => g.Strength);
            int totalAgi = Sockets.Where(g => g != null).Sum(g => g.Agility);
            int totalVit = Sockets.Where(g => g != null).Sum(g => g.Vitality);

            int finalMin = BaseMinDamage + totalStr * 2 + totalAgi * 1;
            int finalMax = BaseMaxDamage + totalStr * 3 + totalAgi * 4;

            return $"{Name}: {finalMin}-{finalMax} Damage, +{totalStr} Strength, +{totalAgi} Agility, +{totalVit} Vitality";
        }
    }

    public class Axe : Weapon
    {
        public Axe(string name, Rarity rarity) : base(name, 5, 10, 4, rarity) { }
    }

    public class Sword : Weapon
    {
        public Sword(string name, Rarity rarity) : base(name, 4, 6, 3, rarity) { }
    }

    public class Knife : Weapon
    {
        public Knife(string name, Rarity rarity) : base(name, 3, 4, 2, rarity) { }
    }

    public class Program
    {
        public static void Main()
        {
            var weapons = new Dictionary<string, Weapon>();
            string input;

            while ((input = Console.ReadLine()) != "END")
            {
                var parts = input.Split(';', StringSplitOptions.RemoveEmptyEntries);
                string command = parts[0];

                if (command == "Create")
                {
                    var rarityAndType = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    Rarity rarity = Enum.Parse<Rarity>(rarityAndType[0]);
                    string type = rarityAndType[1];
                    string name = parts[2];

                    Weapon weapon = type switch
                    {
                        "Axe" => new Axe(name, rarity),
                        "Sword" => new Sword(name, rarity),
                        "Knife" => new Knife(name, rarity),
                        _ => null
                    };

                    if (weapon != null)
                        weapons[name] = weapon;
                }
                else if (command == "Add")
                {
                    string name = parts[1];
                    int index = int.Parse(parts[2]);
                    var gemParts = parts[3].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    Clarity clarity = Enum.Parse<Clarity>(gemParts[0]);
                    string gemType = gemParts[1];

                    Gem gem = gemType switch
                    {
                        "Ruby" => new Ruby(clarity),
                        "Emerald" => new Emerald(clarity),
                        "Amethyst" => new Amethyst(clarity),
                        _ => null
                    };

                    if (weapons.ContainsKey(name))
                        weapons[name].AddGem(index, gem);
                }
                else if (command == "Remove")
                {
                    string name = parts[1];
                    int index = int.Parse(parts[2]);
                    if (weapons.ContainsKey(name))
                        weapons[name].RemoveGem(index);
                }
                else if (command == "Print")
                {
                    string name = parts[1];
                    if (weapons.ContainsKey(name))
                        Console.WriteLine(weapons[name]);
                }
            }
        }
    }
}
