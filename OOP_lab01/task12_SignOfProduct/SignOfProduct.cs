using System;

class SignOfProduct
{
    static void Main()
    {
        Console.WriteLine("enter three numbers: ");
        double a = double.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());
        double c = double.Parse(Console.ReadLine());
        string product="";

        if (a == 0 || b == 0 || c == 0)
        {
            product = "neutral";
        }
        else
        {
            int count = 0;
            if (a < 0) { count++; }
            if (b < 0) { count++; }
            if (c < 0) { count++; }

            if (count == 1 || count == 3) { product = "negative"; }
            else if (count == 2 || count == 0) {product = "positive";}
        }
        Console.WriteLine(product);
    }
}
