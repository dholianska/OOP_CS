using System;

abstract class Vehicle
{
    protected double fuelQuantity;
    protected double fuelConsumption;

    protected Vehicle(double fuelQuantity, double fuelConsumption)
    {
        this.fuelQuantity = fuelQuantity;
        this.fuelConsumption = fuelConsumption;
    }

    public abstract string Drive(double distance);
    public abstract void Refuel(double liters);

    public override string ToString()
    {
        return $"{this.GetType().Name}: {fuelQuantity:F2}";
    }
}

class Car : Vehicle
{
    private const double additionalConsumption = 0.9;

    public Car(double fuelQuantity, double fuelConsumption) : base(fuelQuantity, fuelConsumption)
    {
    }
    public override string Drive(double distance)
    {
        double neededFuel = distance * (fuelConsumption +  additionalConsumption);
        if (neededFuel > fuelQuantity)
        {
            return "car needs refueling";
        }
        fuelQuantity -= neededFuel;
        return $"car travelled {distance} km";
    }
    public override void Refuel(double liters)
    {
        fuelQuantity += liters;
    }
}

class Truck : Vehicle
{
    private const double additionalConsumption = 1.6;
    private const double refuelEfficiency = 0.95;

    public Truck(double fuelQuantity, double fuelConsumption) : base (fuelQuantity, fuelConsumption)
    {
    }
    public override string Drive(double distance)
    {
        double neededFuel = distance * (fuelConsumption + additionalConsumption);
        if (neededFuel > fuelQuantity)
        {
            return "truck needs refueling";
        }
        fuelQuantity -= neededFuel;
        return $"truck travelled {distance} km";
    }
    public override void Refuel(double liters)
    {
        fuelQuantity += liters * refuelEfficiency;
    }
}

class Program
{
    static void Main()
    {
        string[] inputCar = Console.ReadLine().Split();
        string[] inputTruck = Console.ReadLine().Split();

        var car = new Car(double.Parse(inputCar[1]), double.Parse(inputCar[2]));
        var truck = new Truck(double.Parse(inputTruck[1]), double.Parse(inputTruck[2]));

        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            string[] command = Console.ReadLine().Split();
            string action = command[0];
            string type = command[1];
            double value = double.Parse(command[2]);

            if (action.ToLower() == "drive")
            {
                if (type.ToLower() == "car")
                    Console.WriteLine(car.Drive(value));
                else 
                    Console.WriteLine(truck.Drive(value));
            }
            else if (action.ToLower() == "refuel")
            {
                if (type.ToLower() == "car") 
                    car.Refuel(value);
                else 
                    truck.Refuel(value);
            }
        }
        Console.WriteLine(car);
        Console.WriteLine(truck);
    }
}