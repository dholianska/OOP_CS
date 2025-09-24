using System;
using System.Collections.Generic;
using System.Globalization;

/* 2
AudiA4 23 0.3
BMW-M2 45 0.42
Drive BMW-M2 56
Drive AudiA4 5
Drive AudiA4 13
End
*/

class Program
{
    class Car
    {
        public string Model;
        public double FuelAmount;
        public double FuelPerKm;
        public int DistanceTraveled;

        public Car(string model, double fuelAmount, double fuelPerKm)
        {
            Model = model;
            FuelAmount = fuelAmount;
            FuelPerKm = fuelPerKm;
            DistanceTraveled = 0;
        }

        public void Drive(int km)
        {
            double neededFuel = km * FuelPerKm;
            if (neededFuel <= FuelAmount)
            {
                FuelAmount -= neededFuel;
                DistanceTraveled += km;
            }
            else
            {
                Console.WriteLine("not enough fuel for driving");
            }
        }
    }

    static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        List<Car> cars = new List<Car>();

        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string model = input[0];
            double fuelAmount = double.Parse(input[1], CultureInfo.InvariantCulture);
            double fuelPerKm = double.Parse(input[2], CultureInfo.InvariantCulture);

            cars.Add(new Car(model, fuelAmount, fuelPerKm));
        }

        while (true)
        {
            string command = Console.ReadLine();
            if (command == "End") break;

            string[] parts = command.Split();
            if (parts[0] == "Drive")
            {
                string model = parts[1];
                int km = int.Parse(parts[2]);

                foreach (var car in cars)
                {
                    if (car.Model == model)
                    {
                        car.Drive(km);
                        break;
                    }
                }
            }
        }

        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model} {car.FuelAmount:F2} {car.DistanceTraveled}");
        }
    }
}
