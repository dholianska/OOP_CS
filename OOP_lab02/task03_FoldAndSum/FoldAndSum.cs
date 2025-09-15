using System;
using System.Linq;

class FoldAndSum
{
    static void Main()
    {
        int n;
        do
        {
            Console.WriteLine("enter amount of numbers (multiples of 4): ");
            n = int.Parse(Console.ReadLine());
        }
        while (n%4!=0);

        Random random = new Random();
        int[] array = new int[n];
        for (int i=0; i<n; i++)
        {
            array[i] = random.Next(1, 11);
        }
        Console.WriteLine($"your array: {string.Join(' ', array)}");

        int[] array1 = new int[n/2];
        int index1 = 0;
        for (int i =n/4-1; i>=0; i--)
        {
            array1[index1++] = array[i];
        }
        for (int i = n - 1; i>=n-n/4; i--)
        {
            array1[index1++] = array[i];
        }
        Console.WriteLine($"step one: {string.Join(' ', array1)}");

        int[] array2 = new int[n / 2];
        int index2 = 0;
        for (int i=n/4; i<n-n/4; i++)
        {
            array2[index2++] = array[i];
        }
        Console.WriteLine($"step two: {string.Join(' ', array2)}");

        int[] sum = new int[n/2];
        for (int i=0; i<n/2; i++)
        {
            sum[i] = array1[i] + array2[i];
        }
        Console.WriteLine($"result: {string.Join(' ', sum)}");
    }
}