using System;
using System.Linq;

class CompareCharArray
{
    static void Main()
    {
        Console.WriteLine("enter first character array (with spaces): ");
        char[] arr1 = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(char.Parse).ToArray();

        Console.WriteLine("enter second character array (with spaces): ");
        char[] arr2 = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(char.Parse).ToArray();

        int minLength = Math.Min(arr1.Length, arr2.Length);
        bool areEqual = true;

        for (int i = 0; i < minLength; i++)
        {
            if (arr1[i] > arr2[i])
            {
                Console.WriteLine(string.Join("", arr2));
                Console.WriteLine(string.Join("", arr1));
                areEqual = false;
                break;
            }
            else if (arr1[i] < arr2[i])
            {
                Console.WriteLine(string.Join("", arr1));
                Console.WriteLine(string.Join("", arr2));
                areEqual = false;
                break;
            }
        }

        if (areEqual)
        {
            if (arr1.Length < arr2.Length)
            {
                Console.WriteLine(string.Join("", arr1));
                Console.WriteLine(string.Join("", arr2));
            }
            else if (arr1.Length > arr2.Length)
            {
                Console.WriteLine(string.Join("", arr2));
                Console.WriteLine(string.Join("", arr1));
            }
            else
            {
                Console.WriteLine(string.Join("", arr1));
                Console.WriteLine(string.Join("", arr2));
            }
        }
    }
}