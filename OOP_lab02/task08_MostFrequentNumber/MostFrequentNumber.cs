using System;
using System.Linq;

class MostFrequentNumber
{
    static void Main()
    {
        Console.WriteLine("enter the sequence of numbers:");
        int[] array = Console.ReadLine().Split(' ', StringSplitOptions.
            RemoveEmptyEntries).Select(int.Parse).ToArray();

        int maxCount = 0;
        int mostFrequent = 0;

        for (int i = 0; i < array.Length; i++)
        {
            int currentFrequent = array[i];
            int currentCount = 0;

            for (int j = 0; j < array.Length; j++)
            {
                if (array[j] == currentFrequent)
                { currentCount++; }
            }

            if (currentCount > maxCount)
            { maxCount = currentCount;
            mostFrequent = currentFrequent;}
        }

        Console.WriteLine($"the number {mostFrequent} is the most " +
            $"frequent (occurs {maxCount} times)");
    }
}