using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var numbers = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Comparison<int> customComparator = (a, b) =>
        {
            bool aEven = a % 2 == 0;
            bool bEven = b % 2 == 0;

            if (aEven && !bEven) return -1;
            if (!aEven && bEven) return 1;

            return a.CompareTo(b);
        };

        Array.Sort(numbers, customComparator);

        Console.WriteLine(string.Join(" ", numbers));
    }
}