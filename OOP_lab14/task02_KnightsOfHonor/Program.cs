using System;
using System.Linq;
using System.Collections.Generic;

//Pesho Gosho Adasha StanleyRoyce
class Program
{
    static void Main()
    {
        Action<List<string>> sirAction = list =>
        {
            foreach (var item in list)
            {
                Console.WriteLine($"Sir {item}");
            }
        };

        var names = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        sirAction.Invoke(names);
    }
}