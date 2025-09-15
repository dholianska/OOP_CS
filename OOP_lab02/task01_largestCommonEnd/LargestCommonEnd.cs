using System;
using System.Linq;

class LargestCommonEnd
{
    static void Main()
    {
        Console.WriteLine("enter two string arrays: ");
        string[] array1 = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] array2 = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        int minLength = Math.Min(array1.Length, array2.Length);

        int leftCount = 0;
        for (int i =0; i<minLength; i++)
        {
            if (array1[i] == array2[i])
                leftCount++;
            else break;
        }

        int rightCount = 0;
        for (int i=0; i<minLength; i++)
        {
            if (array1[array1.Length - i - 1] == array2[array2.Length - i - 1])
                rightCount++;
            else break;
        }

        if (leftCount == 0 && rightCount == 0)
        {
            Console.WriteLine("немає спільних слів і зліва, і справа");
        }
        else if (leftCount>=rightCount)
        {
            Console.WriteLine($"найбільший кінець є зліва: {string.Join(' ', array1.Take(leftCount))}");
        }
        else
        {
            Console.WriteLine($"найбільший цінець є спрваа: {string.Join(' ', array1.Skip(array1.Length-rightCount))}");
        }
    }
}