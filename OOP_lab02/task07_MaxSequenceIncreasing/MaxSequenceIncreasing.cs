using System;

class MaxSequenceIncreasing
{
    static void Main()
    {
        Console.WriteLine("enter a sequence of numbers:");
        int[] array = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();

        int bestStart = 0;
        int bestLen = 1;
        int currentStart = 0;
        int currentLen = 1;

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[i - 1])
            {
                currentLen++;
            }
            else
            {
                currentStart = i;
                currentLen = 1;
            }

            if (currentLen > bestLen)
            {
                bestLen = currentLen;
                bestStart = currentStart;
            }
        }

        int[] result = array.Skip(bestStart).Take(bestLen).ToArray();
        Console.WriteLine(string.Join(" ", result));
    }
}