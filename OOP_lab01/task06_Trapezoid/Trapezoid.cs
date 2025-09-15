using System;

class Trapezoid
{
    static void Main()
    {
        Console.WriteLine("enter two sides: ");
        double a = double.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());
        Console.WriteLine("enter height: ");
        double h = double.Parse(Console.ReadLine());
        double area = ((a + b) / 2) * h;
        Console.WriteLine("area of trapezoid: ");
        Console.WriteLine(area);
    }
}