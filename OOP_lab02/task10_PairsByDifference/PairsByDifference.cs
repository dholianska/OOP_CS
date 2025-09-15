using System;
using System.Linq;

class PairsByDifference
{
    static void Main()
    {
        Console.WriteLine("enter the sequence of numbers:");
        int[] numbers = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Console.WriteLine("enter difference:");
        int difference = int.Parse(Console.ReadLine());

        int pairCount = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            for (int j = i + 1; j < numbers.Length; j++)
            {
                if (Math.Abs(numbers[i] - numbers[j]) == difference)
                {
                    pairCount++;
                }
            }
        }
        Console.WriteLine(pairCount);
    }
}