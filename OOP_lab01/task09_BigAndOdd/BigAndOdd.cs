using System;

class BigAndOdd
{
    static void Main()
    {
        Console.WriteLine("enter number: ");
        int n = int.Parse(Console.ReadLine());
        bool result;
        if (n>20 && n%2!=0)
        {
            result = true;
        }
        else
        {
            result = false;
        }
        Console.WriteLine(result);

    }
}