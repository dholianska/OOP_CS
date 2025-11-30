namespace P03_FootballBetting.Startup
{
    using P03_FootballBetting.Data;
    using System;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            using (var context = new FootballBettingContext())
            {
                context.Database.EnsureCreated();

                Console.WriteLine("База даних FootballBetting успішно створена!");

            }
        }
    }
}