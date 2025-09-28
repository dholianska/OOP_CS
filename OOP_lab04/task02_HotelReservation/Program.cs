using System;

class Programm
{
    enum Season { Spring, Summer, Autumn, Winter }
    enum DiscountType { VIP, SecondVisit, None }
    class PriceCalculator
    {
        public double PricePerDay { get; set; }
        public int AmountOfDays { get; set; }
        public Season Season { get; set; }
        public DiscountType Discount { get; set; }
        public double CalculateTotalPrice()
        {
            double multiplier = Season switch
            {
                Season.Autumn => 1,
                Season.Spring => 2,
                Season.Winter => 3,
                Season.Summer => 4,
                _ => 1
            };

            double totalPrice = PricePerDay * AmountOfDays * multiplier;

            double discountPercent = Discount switch
            {
                DiscountType.VIP => 0.20,
                DiscountType.SecondVisit => 0.10,
                DiscountType.None => 0,
                _ => 0
            };

            return Math.Round(totalPrice - totalPrice * discountPercent, 2);
        }
    }
    static void Main()
    {
        string input = Console.ReadLine();
        string[] parts = input.Split(' ');

        double pricePerDay = double.Parse(parts[0]);
        int numberOfDays = int.Parse(parts[1]);
        Season season = Enum.Parse<Season>(parts[2], true);

        DiscountType discount = DiscountType.None;
        if (parts.Length > 3)
            discount = Enum.Parse<DiscountType>(parts[3], true);

        PriceCalculator calculator = new PriceCalculator
        {
            PricePerDay = pricePerDay,
            AmountOfDays = numberOfDays,
            Season = season,
            Discount = discount
        };

        Console.WriteLine($"{calculator.CalculateTotalPrice():F2}");
    }
}