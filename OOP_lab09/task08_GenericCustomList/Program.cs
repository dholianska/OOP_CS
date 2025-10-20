using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class CustomList<T> : IEnumerable<T> where T : IComparable<T>
{
    private List<T> items = new List<T>();

    public void Add(T element) => items.Add(element);

    public T Remove(int index)
    {
        T element = items[index];
        items.RemoveAt(index);
        return element;
    }

    public bool Contains(T element) => items.Contains(element);

    public void Swap(int index1, int index2)
    {
        (items[index1], items[index2]) = (items[index2], items[index1]);
    }

    public int CountGreaterThan(T element) =>
        items.Count(x => x.CompareTo(element) > 0);

    public T Max() => items.Max();

    public T Min() => items.Min();

    public void Print()
    {
        foreach (var item in this)
        {
            Console.WriteLine(item);
        }
    }

    public List<T> Items
    {
        get
        {
            return items;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in items)
            yield return item;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Sorter
{
    public static void Sort<T>(CustomList<T> list) where T : IComparable<T>
    {
        
        list.Items.Sort();
    }
}

class Program
{
    static void Main()
    {
        var list = new CustomList<string>();
        string input;

        while ((input = Console.ReadLine()) != "END")
        {
            var parts = input.Split();

            string command = parts[0];

            switch (command)
            {
                case "Add":
                    list.Add(parts[1]);
                    break;

                case "Remove":
                    list.Remove(int.Parse(parts[1]));
                    break;

                case "Contains":
                    Console.WriteLine(list.Contains(parts[1]));
                    break;

                case "Swap":
                    list.Swap(int.Parse(parts[1]), int.Parse(parts[2]));
                    break;

                case "Greater":
                    Console.WriteLine(list.CountGreaterThan(parts[1]));
                    break;

                case "Max":
                    Console.WriteLine(list.Max());
                    break;

                case "Min":
                    Console.WriteLine(list.Min());
                    break;

                case "Print":
                    list.Print();
                    break;

                case "Sort":
                    Sorter.Sort(list);
                    break;
            }
        }
    }
}
