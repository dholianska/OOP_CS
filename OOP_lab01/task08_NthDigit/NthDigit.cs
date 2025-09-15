using System;

class NthDigit
{
    static void Main()
    {
        Console.WriteLine("enter number: ");
        int number = int.Parse(Console.ReadLine());
        Console.WriteLine("enter n: ");
        int n = int.Parse(Console.ReadLine());
        int nDigit = (int)(number / Math.Pow(10, n - 1)) % 10;
        if (nDigit != 0)
        {
            Console.WriteLine(nDigit);
        }
        else
        {
            Console.WriteLine("-");
        }


    }
}