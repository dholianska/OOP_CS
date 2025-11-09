using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var names = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .ToArray();
        Predicate<string> isLongEnough = name => name.Length <= n;
        var selectedNames = names.Where(nm => isLongEnough(nm)).ToArray();
        Array.ForEach(selectedNames, item => Console.WriteLine(item));
    }
}