using System;

class Average
{ 
    static void Main()
    {
        int a, b, c;
        double average;
        Console.WriteLine("enter three numbers: ");
        a = int.Parse(Console.ReadLine());
        b = int.Parse(Console.ReadLine());
        c = int.Parse(Console.ReadLine());
        average = (a + b + c)*1.0 / 3;
        Console.WriteLine(average);
    }
}
