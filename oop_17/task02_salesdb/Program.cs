using P03_SalesDatabase.Data;
using System;
using System.Linq;

namespace P03_SalesDatabase
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new SalesContext())
            {
                Console.WriteLine("--- Sales Database Setup ---");

                context.Database.EnsureCreated();

                context.Seed();

                Console.WriteLine("База даних 'SalesDB' успішно створена та заповнена випадковими даними.");

              var salesCount = context.Sales.Count();
                Console.WriteLine($"Додано {salesCount} продажів.");
            }
        }
    }
}