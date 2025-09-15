using System;
using System.Linq;

class RotateAndSum
{
    static void Main()
    {
        Console.WriteLine("enter array of numbers: ");
        int[] array = Console.ReadLine()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse).ToArray();
        
        Console.WriteLine($"your array: {string.Join(", ", array)}");
        Console.WriteLine("enter K for rotating: ");
        int k = int.Parse(Console.ReadLine());
        int n = array.Length;

        int[] sum = new int[n];
        int[] current = (int[])array.Clone();

        for (int rot = 0; rot < k; rot++)
        {
            int[] rotated = new int[n];

            for (int i = 0; i < n; i++)
            {
                int newIndex = (i + 1) % n;
                rotated[newIndex] = current[i];
            }

            for (int i=0; i<n; i++)
            {
                sum[i] += rotated[i];
            }

            current = rotated;
        }
        Console.WriteLine(string.Join(" ", current));
        Console.WriteLine(string.Join(" ", sum));

    } 
}
   
