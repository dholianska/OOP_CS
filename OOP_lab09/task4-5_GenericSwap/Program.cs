using System;
using System.Collections.Generic;

public class Box<T>
{
    public T Value { get; }

    public Box(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{typeof(T).FullName}: {Value}";
    }

    public static void Swap(List<Box<T>> list, int index1, int index2)
    {
        var temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var stringList = new List<Box<string>>();
        var intList = new List<Box<int>>();
        bool allAreNumbers = true;

        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int number))
                intList.Add(new Box<int>(number));
            else
            {
                allAreNumbers = false;
                stringList.Add(new Box<string>(input));
            }
        }

        var indices = Console.ReadLine().Split();
        int firstIndex = int.Parse(indices[0]);
        int secondIndex = int.Parse(indices[1]);

        if (allAreNumbers)
            Box<int>.Swap(intList, firstIndex, secondIndex);
        else
            Box<string>.Swap(stringList, firstIndex, secondIndex);

        if (allAreNumbers)
            foreach (var box in intList)
                Console.WriteLine(box);
        else
            foreach (var box in stringList)
                Console.WriteLine(box);
    }
}
