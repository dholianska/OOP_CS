using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> guests = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        string command;
        while ((command = Console.ReadLine()) != "Party!")
        {
            string[] parts = command.Split();
            string action = parts[0];  
            string criteria = parts[1];
            string param = parts[2];

            Predicate<string> predicate = criteria switch
            {
                "StartsWith" => name => name.StartsWith(param),
                "EndsWith" => name => name.EndsWith(param),
                "Length" => name => name.Length == int.Parse(param),
                _ => _ => false
            };

            if (action == "Remove")
                guests.RemoveAll(predicate);
            else if (action == "Double")
            {
                var matches = guests.Where(name => predicate(name)).ToList();
                foreach (var m in matches)
                    guests.Add(m);
            }
        }

        if (guests.Count > 0)
            Console.WriteLine($"{string.Join(", ", guests)} are going to the party!");
        else
            Console.WriteLine("Nobody is going to the party!");
    }
}
