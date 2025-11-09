using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Func<int[], int> minFunc = array => array.Min();

        string input = Console.ReadLine();
        int[] numbers = input.Split(' ').Select(int.Parse).ToArray();

        int minimum = minFunc(numbers);
        Console.WriteLine(minimum);

    }

}