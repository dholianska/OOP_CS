using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    private string _name;
    private decimal _cost;

    public string Name
    {
        get { return _name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("name cant be empty");
            _name = value;
        }
    }

    public decimal Cost
    {
        get { return _cost; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("money cant be negative");
            _cost = value;
        }
    }

    public Product(string name, decimal cost)
    {
        Name = name;
        Cost = cost;
    }

    public override string ToString() { return Name; }
}

class Person
{
    private string _name;
    private decimal _money;
    private List<Product> _bag;

    public string Name
    {
        get {return _name; }
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("name cant be empty");
            _name = value;
        }
    }

    public decimal Money
    {
        get { return _money; }
        private set
        {
            if (value < 0)
                throw new ArgumentException("money cant be negative");
            _money = value;
        }
    }

    public IReadOnlyList<Product> Bag => _bag;

    public Person(string name, decimal money)
    {
        Name = name;
        Money = money;
        _bag = new List<Product>();
    }

    public void Buy(Product product)
    {
        if (Money >= product.Cost)
        {
            Money -= product.Cost;
            _bag.Add(product);
            Console.WriteLine($"{Name} bought {product.Name}");
        }
        else
        {
            Console.WriteLine($"{Name} can't afford {product.Name}");
        }
    }

    public override string ToString()
    {
        if (_bag.Count == 0)
        {
            return $"{Name} - Nothing bought";
        }
        else
        {
            return $"{Name} - {string.Join(", ", _bag)}";
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            string[] peopleInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            List<Person> people = new List<Person>();
            foreach (var item in peopleInput)
            {
                string[] parts = item.Split('=');
                string name = parts[0];
                decimal money = decimal.Parse(parts[1]);
                people.Add(new Person(name, money));
            }

            string[] productsInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            List<Product> products = new List<Product>();
            foreach (var item in productsInput)
            {
                string[] parts = item.Split('=');
                string name = parts[0];
                decimal cost = decimal.Parse(parts[1]);
                products.Add(new Product(name, cost));
            }

            string command;
            while ((command = Console.ReadLine()) != "END")
            {
                string[] parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string personName = parts[0];
                string productName = parts[1];

                Person buyer = null;
                Product product = null;

                foreach (var p in people)
                    if (p.Name == personName) buyer = p;

                foreach (var pr in products)
                    if (pr.Name == productName) product = pr;

                if (buyer != null)
                    buyer.Buy(product);

            }

            foreach (var person in people)
            {
                Console.WriteLine(person);
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}