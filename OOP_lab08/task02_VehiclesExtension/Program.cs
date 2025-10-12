using System;

abstract class Vehicle
{
    protected double fuelQuantity;
    protected double fuelConsumption;
    protected double tankCapacity;

    protected Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
    {
        this.fuelQuantity = fuelQuantity;
        this.fuelConsumption = fuelConsumption;
        this.tankCapacity = tankCapacity;
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

    public Car(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }
    public override string Drive(double distance)
    {
        double neededFuel = distance * (fuelConsumption + additionalConsumption);
        if (neededFuel > fuelQuantity)
        {
            return "car needs refueling";
        }
        fuelQuantity -= neededFuel;
        return $"car travelled {distance} km";
    }
    public override void Refuel(double liters)
    {
        if (fuelQuantity + liters > tankCapacity)
        {
            Console.WriteLine($"cannot fit {liters} fuel in the tank");
            return;
        }
        if (liters <= 0 )
        {
            Console.WriteLine("fuel must be a positive number");
            return;
        }
            
        fuelQuantity += liters;
    }
}

class Truck : Vehicle
{
    private const double additionalConsumption = 1.6;
    private const double refuelEfficiency = 0.95;

    public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
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
        if (fuelQuantity + liters > tankCapacity)
        {
            Console.WriteLine($"cannot fit {liters} fuel in the tank");
            return;
        }
        if (liters <= 0 )
        {
            Console.WriteLine("fuel must be a positive number");
            return;
        }
        fuelQuantity += liters * refuelEfficiency;
    }
}

class Bus : Vehicle
{
    private const double additionalConsumption = 1.4;

    public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity) : base(fuelQuantity, fuelConsumption, tankCapacity)
    {
    }
    public override string Drive(double distance)
    {
        double neededFuel = distance * (fuelConsumption + additionalConsumption);
        if (neededFuel > fuelQuantity)
        {
            return "bus needs refueling";
        }
        fuelQuantity -= neededFuel;
        return $"bus travelled {distance} km";
    }
    public string DriveEmpty(double distance)
    {
        double neededFuel = distance * fuelConsumption;
        if (neededFuel > fuelQuantity)
        {
            return "bus needs refueling";
        }
        fuelQuantity -= neededFuel;
        return $"bus travelled {distance} km";
    }
    public override void Refuel(double liters)
    {
        if (fuelQuantity + liters > tankCapacity)
        {
            Console.WriteLine($"cannot fit {liters} fuel in the tank");
            return;
        }
        if (liters <= 0)
        {
            Console.WriteLine("fuel must be a positive number");
            return;
        }
        fuelQuantity += liters;
    }
}

class Program
{
    static void Main()
    {
        string[] inputCar = Console.ReadLine().Split();
        string[] inputTruck = Console.ReadLine().Split();
        string[] inputBus = Console.ReadLine().Split();

        var car = new Car(double.Parse(inputCar[1]), double.Parse(inputCar[2]), double.Parse(inputCar[3]));
        var truck = new Truck(double.Parse(inputTruck[1]), double.Parse(inputTruck[2]), double.Parse(inputTruck[3]));
        var bus = new Bus(double.Parse(inputBus[1]), double.Parse(inputBus[2]), double.Parse(inputBus[3]));

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
                else if (type.ToLower() == "truck")
                    Console.WriteLine(truck.Drive(value));
                else 
                    Console.WriteLine(bus.Drive(value));
            }
            else if (action.ToLower() == "driveempty")
            {
                Console.WriteLine(bus.DriveEmpty(value));
            }
            else if (action.ToLower() == "refuel")
            {
                if (type.ToLower() == "car")
                    car.Refuel(value);
                else if (type.ToLower() == "truck")
                    truck.Refuel(value);
                else 
                    bus.Refuel(value);
            }
        }
        Console.WriteLine(car);
        Console.WriteLine(truck);
        Console.WriteLine(bus);
    }
}