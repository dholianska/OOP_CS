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
        int n = int.Parse(Console.ReadLine());
        Predicate<int> isDivisible = x => x % n == 0;
        Func<int[], int[]> filtring = array => array.Where(e => !isDivisible(e))
                                                    .Reverse().ToArray();
        Action<int[]> print = array => Array.ForEach(array, item => Console.Write($"{item} "));

        int[] modifiedNumbers = filtring(numbers);
        print(modifiedNumbers);
        
    }
}