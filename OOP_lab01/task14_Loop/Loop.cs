using System;

class Loop
{
    static void Main()
    {
        Console.WriteLine("enter your number: ");
        int n = int.Parse(Console.ReadLine());
        int factorial = 1;
        for (int i = 2; i<=n; i++)
        {
            factorial *= i;
        }
        Console.WriteLine($"{n}! = {factorial}");
    }
}