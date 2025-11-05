using System;
using System.Collections.Generic;
using System.Linq;

class Order
{
    public string Company { get; set; }
    public string Product { get; set; }
    public int Amount { get; set; }

    public Order(string company, string product, int amount)
    {
        Company = company;
        Product = product;
        Amount = amount;
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        var orders = new List<Order>();

        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine().Trim('|', ' ');
            string[] parts = input.Split(" - ", StringSplitOptions.RemoveEmptyEntries);

            string company = parts[0];
            int amount = int.Parse(parts[1]);
            string product = parts[2];

            orders.Add(new Order(company, product, amount));
        }
        var grouped = orders
            .GroupBy(o => o.Company)
            .OrderBy(g => g.Key)
            .Select(g => new
            {
                Company = g.Key,
                Products = g
                    .GroupBy(o => o.Product)
                    .Select(p => new { Product = p.Key, Total = p.Sum(x => x.Amount) })
                    .ToList()
            });

        foreach (var company in grouped)
        {
            string productsString = string.Join(", ", company.Products.Select(p => $"{p.Product}-{p.Total}"));
            Console.WriteLine($"{company.Company}: {productsString}");
        }
    }
}
