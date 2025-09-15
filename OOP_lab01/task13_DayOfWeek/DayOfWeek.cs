using System;

class DayOfWeek
{
    static void Main()
    {
        Console.WriteLine("enter number of the day: ");
        int n = int.Parse(Console.ReadLine());
        switch(n)
        {
            case 1:
                Console.WriteLine("monday");
                break;
            case 2:
                Console.WriteLine("tuesday");
                break;
            case 3:
                Console.WriteLine("wednesday");
                break;
            case 4:
                Console.WriteLine("thursday");
                break;
            case 5:
                Console.WriteLine("friday");
                break;
            case 6:
                Console.WriteLine("saturday");
                break;
            case 7:
                Console.WriteLine("sunday");
                break;
            default:
                Console.WriteLine("incorrect value");
                break;
        }
    }
}