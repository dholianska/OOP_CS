using System;
using System.Collections;
using System.Collections.Generic;

class ListyIterator<T> : IEnumerable<T>
{
    private List<T> items;
    private int index;

    public ListyIterator(params T[] elements)
    {
        items = new List<T>(elements);
        index = 0;
    }

    public bool HasNext()
    {
        return index + 1 < items.Count;
    }
    public bool Move()
    {
        if (HasNext())
        {
            index++;
            return true;
        }

        return false;
    }
    public void Print()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("invalid operation!");
        }
        Console.WriteLine(items[index]);
    }

    public void PrintAll()
    {
        Console.WriteLine(string.Join(' ', this));
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in items)
        {
            yield return item;
        }
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

class Program
{
    static void Main()
    {
        ListyIterator<string> listy = null;

        while (true)
        {
            string input = Console.ReadLine();
            if (input == "END") break;

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts[0];

            try
            {
                switch (command)
                {
                    case "Create":
                        if (parts.Length > 1)
                        {
                            listy = new ListyIterator<string>(parts[1..]);
                        }
                        else
                        {
                            listy = new ListyIterator<string>();
                        }
                        break;
                    case "Move":
                        Console.WriteLine(listy.Move());
                        break;
                    case "Print":
                        listy.Print();
                        break;
                    case "HasNext":
                        Console.WriteLine(listy.HasNext());
                        break;
                    case "PrintAll":
                        listy.PrintAll();
                        break;
                    default:
                        throw new InvalidOperationException("invalid operation!");
                        break;
                }
            }
            catch (Exception ie)
            {
                Console.WriteLine(ie.Message);
            }
        }
    }
}