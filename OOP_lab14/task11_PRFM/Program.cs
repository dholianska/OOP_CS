using System;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<string> guests = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
        Dictionary<string, Predicate<string>> filters = new();

        string command;
        while ((command = Console.ReadLine()) != "Print")
        {
            var parts = command.Split(';');
            string action = parts[0];
            string type = parts[1];
            string param = parts[2];

            string key = $"{type};{param}";

            Predicate<string> predicate = type switch
            {
                "Starts with" => name => name.StartsWith(param),
                "Ends with" => name => name.EndsWith(param),
                "Length" => name => name.Length == int.Parse(param),
                "Contains" => name => name.Contains(param),
                _ => _ => false
            };

            if (action == "Add filter")
                filters[key] = predicate;
            else if (action == "Remove filter")
                filters.Remove(key);
        }

        foreach (var filter in filters.Values)
            guests.RemoveAll(filter);

        Console.WriteLine(string.Join(" ", guests));
    }
}
