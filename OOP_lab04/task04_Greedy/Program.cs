using System;
using System.Collections.Generic;
using System.Linq;

class Item
{
    public string Name { get; }
    public long Quantity { get; private set; }
    public string Type { get; }

    public Item(string name, long quantity, string type)
    {
        Name = name;
        Quantity = quantity;
        Type = type;
    }

    public void Add(long amount)
    {
        Quantity += amount;
    }
}

class Bag
{
    private long Capacity;
    private long Load;
    private List<Item> items = new List<Item>();

    public Bag(long capacity)
    {
        Capacity = capacity;
        Load = 0;
    }

    public bool TryAdd(Item newItem)
    {
        if (Load + newItem.Quantity > Capacity) return false;

        long gold = Total("Gold");
        if (newItem.Type == "Gold") gold += newItem.Quantity;

        long gems = Total("Gem");
        if (newItem.Type == "Gem") gems += newItem.Quantity;

        long cash = Total("Cash");
        if (newItem.Type == "Cash") cash += newItem.Quantity;

        if (gems > gold || cash > gems) return false;

        var exist = items.FirstOrDefault(i => i.Name == newItem.Name);
        if (exist != null)
        {
            exist.Add(newItem.Quantity);
        }
        else
        {
            items.Add(newItem);
        }

        Load += newItem.Quantity;
        return true;
    }

    private long Total(string type)
    {
        long sum = 0;
        foreach (var i in items)
        {
            if (i.Type == type) sum += i.Quantity;
        }
        return sum;
    }

    public void Print()
    {
        foreach (var group in items.GroupBy(i => i.Type)
                                   .OrderByDescending(g => g.Sum(x => x.Quantity)))
        {
            Console.WriteLine($"<{group.Key}> ${group.Sum(x => x.Quantity)}");
            foreach (var item in group.OrderByDescending(i => i.Name)
                                      .ThenBy(i => i.Quantity))
            {
                Console.WriteLine($"##{item.Name} - {item.Quantity}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        long capacity = long.Parse(Console.ReadLine());
        string[] input = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var bag = new Bag(capacity);

        for (int i = 0; i < input.Length; i += 2)
        {
            string name = input[i];
            long qty = long.Parse(input[i + 1]);
            string type = GetType(name);
            if (type != null)
            {
                bag.TryAdd(new Item(name, qty, type));
            }
        }

        bag.Print();
    }

    static string GetType(string name)
    {
        if (name.Equals("Gold", StringComparison.OrdinalIgnoreCase))
        {
            return "Gold";
        }

        if (name.Length == 3)
        {
            return "Cash";
        }

        if (name.Length >= 4 && name.EndsWith("Gem", StringComparison.OrdinalIgnoreCase))
        {
            return "Gem";
        }

        return null;
    }
}
