using System;

class Chicken
{
    private string name;
    private int age;

    public Chicken(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public string Name
    {
        get { return name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("name cant be empty");
            name = value;
        }
    }

    public int Age
    {
        get { return age; }
        private set
        {
            if (value < 0 || value > 15)
                throw new ArgumentException("age has to be in a range from 0 to 15");
            age = value;
        }
    }
    public double ProductPerDay
    {
        get
        {
            return CalculateProductPerDay();
        }
    }
    private double CalculateProductPerDay()
    {
        if (age < 6) return 2;
        if (age <= 12) return 1;
        return 0.75;
    }
    public override string ToString()
    {
        return $"Chicken {Name} (age {Age}) can produce {ProductPerDay} eggs per day.";
    }
}

class Program
{
    static void Main()
    {
        try
        {
            string name = Console.ReadLine();
            int age = int.Parse(Console.ReadLine());

            Chicken chicken = new Chicken(name, age);
            Console.WriteLine(chicken);
        }
        catch (ArgumentException err)
        {
            Console.WriteLine(err.Message);
        }
    }
}