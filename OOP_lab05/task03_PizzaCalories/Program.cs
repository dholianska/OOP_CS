using System;

class Dough
{
    private string _flourType; 
    private string _bakingTechnique;
    private double _weight;

    public string FlourType
    {
        get => _flourType;
        private set
        {
            if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                throw new ArgumentException("invalid type of dough.");
            _flourType = value;
        }
    }

    public string BakingTechnique
    {
        get => _bakingTechnique;
        private set
        {
            if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                throw new ArgumentException("invalid type of dough.");
            _bakingTechnique = value;
        }
    }

    public double Weight
    {
        get => _weight;
        private set
        {
            if (value < 1 || value > 200)
                throw new ArgumentException("dough weight should be in the range [1..200].");
            _weight = value;
        }
    }

    public Dough(string flourType, string bakingTechnique, double weight)
    {
        FlourType = flourType;
        BakingTechnique = bakingTechnique;
        Weight = weight;
    }

    public double Calories
    {
        get
        {
            double flourModifier = 1.0;
            if (FlourType.ToLower() == "white") flourModifier = 1.5;
            else if (FlourType.ToLower() == "wholegrain") flourModifier = 1.0;

            double techniqueModifier = 1.0;
            if (BakingTechnique.ToLower() == "crispy") techniqueModifier = 0.9;
            else if (BakingTechnique.ToLower() == "chewy") techniqueModifier = 1.1;
            else if (BakingTechnique.ToLower() == "homemade") techniqueModifier = 1.0;

            return (2 * Weight) * flourModifier * techniqueModifier;
        }
    }
}

class Topping
{
    private string _type;
    private double _weight;

    public Topping(string type, double weight)
    {
        Type = type;
        Weight = weight;
    }

    public string Type
    {
        get => _type;
        private set
        {
            string lower = value.ToLower();
            if (lower != "meat" && lower != "veggies" &&
                lower != "cheese" && lower != "sauce")
            {
                throw new ArgumentException($"cannot place {value} on top of your pizza.");
            }
            _type = value;
        }
    }

    public double Weight
    {
        get => _weight;
        private set
        {
            if (value < 1 || value > 50)
            {
                throw new ArgumentException($"{Type} weight should be in the range [1..50].");
            }
            _weight = value;
        }
    }

    public double Calories
    {
        get
        {
            double modifier = 1.0;
            switch (Type.ToLower())
            {
                case "meat": modifier = 1.2; break;
                case "veggies": modifier = 0.8; break;
                case "cheese": modifier = 1.1; break;
                case "sauce": modifier = 0.9; break;
            }
            return (2 * Weight) * modifier;
        }
    }
}

class Pizza
{
    private string _name;
    private List<Topping> _toppings;

    public Pizza(string name)
    {
        Name = name;
        _toppings = new List<Topping>();
    }

    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
            {
                throw new ArgumentException("pizza name should be between 1 and 15 symbols.");
            }
            _name = value;
        }
    }

    public Dough Dough { get; set; }

    public IReadOnlyCollection<Topping> Toppings => _toppings.AsReadOnly();

    public void AddTopping(Topping topping)
    {
        if (_toppings.Count >= 10)
        {
            throw new ArgumentException("number of toppings should be in range [0..10].");
        }
        _toppings.Add(topping);
    }

    public double TotalCalories
    {
        get
        {
            double sum = Dough.Calories;
            foreach (var topping in _toppings)
            {
                sum += topping.Calories;
            }
            return sum;
        }
    }
}
class Program
{
    static void Main()
    {
        try
        {
            /*
            string[] toppingInput = Console.ReadLine().Split();
            Topping topping = new Topping(toppingInput[1], double.Parse(toppingInput[2]));
            Console.WriteLine($"{topping.Calories:F2}");*/

            string[] pizzaInput = Console.ReadLine().Split();
            Pizza pizza = new Pizza(pizzaInput[1]);

            string[] doughInput = Console.ReadLine().Split();
            Dough dough = new Dough(doughInput[1], doughInput[2], double.Parse(doughInput[3]));
            pizza.Dough = dough;

            string line;
            while ((line = Console.ReadLine()) != "END")
            {
                string[] parts = line.Split();
                if (parts[0] == "Topping")
                {
                    Topping topping = new Topping(parts[1], double.Parse(parts[2]));
                    pizza.AddTopping(topping);
                }
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:F2} Calories.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

