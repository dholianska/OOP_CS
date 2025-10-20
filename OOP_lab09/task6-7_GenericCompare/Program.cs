using System;
using System.Collections.Generic;

class Box<T>
{
    public T Value { get; }
    public Box(T value) { Value = value; }

    public override string ToString() => $"{typeof(T)}: {Value}";

    public static int CountGreaterThan(List<Box<T>> list, T element)
    {
        int count = 0;
        foreach (var box in list)
        {
            if (double.TryParse(box.Value.ToString(), out double num1) &&
                double.TryParse(element.ToString(), out double num2))
            {
                if (num1 > num2) count++;
            }
            else
            {
                if (string.Compare(box.Value.ToString(), element.ToString()) > 0)
                    count++;
            }
        }
        return count;
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var list = new List<Box<string>>();

        for (int i = 0; i < n; i++)
        {
            list.Add(new Box<string>(Console.ReadLine()));
        }

        string element = Console.ReadLine();
        Console.WriteLine(Box<string>.CountGreaterThan(list, element));
    }
}
