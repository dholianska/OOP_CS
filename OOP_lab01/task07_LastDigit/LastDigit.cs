using System;

class LastDigit
{ 
    static void Main()
    {
        int lastDigit;
        Console.WriteLine("enter number: ");
        int n = int.Parse(Console.ReadLine());
        lastDigit = n % 10; //n Mod 10
        Console.WriteLine(lastDigit);
    }
}
