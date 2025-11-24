namespace P03_SalesDatabase.Data
{
    using P03_SalesDatabase.Data.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public static class SalesContextExtensions
    {
        public static void Seed(this SalesContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any() || context.Customers.Any() || context.Stores.Any() || context.Sales.Any())
            {
                return;
            }

            var random = new Random();

            var stores = CreateStores();
            var customers = CreateCustomers();
            var products = CreateRandomProducts(random);

            context.Stores.AddRange(stores);
            context.Customers.AddRange(customers);
            context.Products.AddRange(products);
            context.SaveChanges();

            var sales = CreateRandomSales(random, customers, products, stores);
            context.Sales.AddRange(sales);
            context.SaveChanges();
        }


        private static List<Store> CreateStores()
        {
            return new List<Store>
            {
                new Store { Name = "Філіал Одеса" },
                new Store { Name = "Філіал Харків" },
                new Store { Name = "Філіал Запоріжжя" }
            };
        }

        private static List<Customer> CreateCustomers()
        {
            return new List<Customer>
            {
                new Customer { Name = "Ігор Антоненко", Email = "igor@mail.com", CreaditCardNumber = "1234567812345678" },
                new Customer { Name = "Катерина Біла", Email = "katya@mail.com", CreaditCardNumber = "8765432187654321" },
                new Customer { Name = "Дмитро Гнатюк", Email = "dima@mail.com", CreaditCardNumber = "1111222233334444" }
            };
        }

        private static List<Product> CreateRandomProducts(Random random)
        {
            var productNames = new[] { "Кава", "Чай", "Цукор", "Борошно", "Олія", "Сіль" };
            var products = new List<Product>();

            for (int i = 0; i < 15; i++)
            {
                products.Add(new Product
                {
                    Name = productNames[random.Next(productNames.Length)] + $" ({i + 1})",
                    Quantity = random.Next(1, 100) * random.NextDouble(),
                    Price = random.Next(10, 500) + (decimal)random.NextDouble() * 5m
                });
            }
            return products;
        }

        private static List<Sale> CreateRandomSales(Random random, List<Customer> customers, List<Product> products, List<Store> stores)
        {
            var sales = new List<Sale>();
            for (int i = 0; i < 30; i++)
            {
                sales.Add(new Sale
                {
                    Customer = customers[random.Next(customers.Count)],
                    Product = products[random.Next(products.Count)],
                    Store = stores[random.Next(stores.Count)],
                    Date = DateTime.Now.AddDays(-random.Next(1, 90))
                });
            }
            return sales;
        }
    }
}