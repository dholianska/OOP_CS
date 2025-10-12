using System;
using System.Collections.Generic;

abstract class Food
{
    public int Quantity { get; set; }
    protected Food(int quantity)
    {
        Quantity = quantity;
    }
}
class Vegetable : Food { public Vegetable(int quantity) :  base(quantity) { } }
class Fruit : Food { public Fruit(int quantity) : base(quantity) { } }
class Meat : Food { public Meat(int quantity) : base(quantity) { } }
class Seeds : Food { public Seeds(int quantity) : base(quantity) { } }

abstract class Animal
{
    protected string Name { get; }
    protected double Weight { get; set; }
    protected int FoodEaten { get; set; }
    protected Animal(string name, double weight, int foodEaten = 0)
    {
        Name = name;
        Weight = weight;
        FoodEaten = foodEaten;
    }
    public abstract void Sound();
    public abstract void EatFood(Food food);
}

abstract class Bird : Animal
{
    protected double WingSize {  get; }
    protected Bird(string name, double weight, int foodEaten, double wingSize) : base(name, weight, foodEaten)
    {
        WingSize = wingSize;
    }
    public override string ToString()
    {
        return $"{GetType().Name} [{Name}, {WingSize}, {Weight}, {FoodEaten}]";
    }
}
class Owl : Bird
{
    public override void Sound()
    {
        Console.WriteLine("hoot hoot");
    }
    public Owl(string name, double weight, int foodEaten, double wingSize) : base(name, weight, foodEaten, wingSize) { }
    public override void EatFood(Food food)
    {
        if (food is Meat)
        {
            FoodEaten += food.Quantity;
            Weight += 0.25 * food.Quantity;
        }
        else
        {
            Console.WriteLine($"owl does not eat {food.GetType().Name}!");
        }
    }
}
class Hen : Bird
{
    public override void Sound()
    {
        Console.WriteLine("cluck");
    }
    public Hen(string name, double weight, int foodEaten, double wingSize) : base(name, weight, foodEaten, wingSize) { }
    public override void EatFood(Food food)
    {
            FoodEaten += food.Quantity;
            Weight += 0.35 * food.Quantity;
    }
}

abstract class Mammal : Animal
{
    protected string LivingRegion { get; }
    protected Mammal(string name, double weight, int foodEaten, string livingRegion) : base(name, weight, foodEaten)
    {
        LivingRegion = livingRegion ;
    }
}
class Mouse : Mammal
{
    public override void Sound()
    {
        Console.WriteLine("squeak");
    }
    public Mouse(string name, double weight, int foodEaten, string livingRegion) : base(name, weight, foodEaten, livingRegion) { }
    public override void EatFood(Food food)
    {
        if (food is Vegetable || food is Fruit)
        {
            FoodEaten += food.Quantity;
            Weight += 0.10 * food.Quantity;
        }
        else
        {
            Console.WriteLine($"mouse does not eat {food.GetType().Name}!");
        }
    }
    public override string ToString()
    {
        return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
class Dog : Mammal
{
    public override void Sound()
    {
        Console.WriteLine("woof!");
    }
    public Dog(string name, double weight, int foodEaten, string livingRegion) : base(name, weight, foodEaten, livingRegion) { }
    public override void EatFood(Food food)
    {
        if (food is Meat)
        {
            FoodEaten += food.Quantity;
            Weight += 0.40 * food.Quantity;
        }
        else
        {
            Console.WriteLine($"dog does not eat {food.GetType().Name}!");
        }
    }
    public override string ToString()
    {
        return $"{GetType().Name} [{Name}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
abstract class Feline : Mammal
{
    protected string Breed { get; }
    protected Feline(string name, double weight, int foodEaten, string livingRegion, string breed) : base(name, weight, foodEaten, livingRegion) 
    {
        Breed = breed ;
    }
    public override string ToString()
    {
        return $"{GetType().Name} [{Name}, {Breed}, {Weight}, {LivingRegion}, {FoodEaten}]";
    }
}
class Cat : Feline
{
    public override void Sound()
    {
        Console.WriteLine("meow");
    }
    public Cat(string name, double weight, int foodEaten, string livingRegion, string breed) : base(name, weight, foodEaten, livingRegion, breed){ }
    public override void EatFood(Food food)
    {
        if (food is Meat || food is Vegetable)
        {
            FoodEaten += food.Quantity;
            Weight += 0.30 * food.Quantity;
        }
        else
        {
            Console.WriteLine($"cat does not eat {food.GetType().Name}!");
        }
    }
}
class Tiger : Feline
{
    public override void Sound()
    {
        Console.WriteLine("ROAR!!!");
    }
    public Tiger(string name, double weight, int foodEaten, string livingRegion, string breed) : base(name, weight, foodEaten, livingRegion, breed) { }
    public override void EatFood(Food food)
    {
        if (food is Meat)
        {
            FoodEaten += food.Quantity;
            Weight += 1.00 * food.Quantity;
        }
        else
        {
            Console.WriteLine($"tiger does not eat {food.GetType().Name}!");
        }
    }
}

class Program
{
    static void Main()
    {
        try
        {
            string line;
            int i = 0;
            var animals = new List<Animal>();
            var meals = new List<Food>();
            while ((line = Console.ReadLine()) != "End")
            {
                if (i % 2 == 0)
                {
                    string[] animalInfo = line.Split();
                    string type = animalInfo[0];
                    Animal animal = type switch
                    {
                        "Owl" => new Owl(animalInfo[1], double.Parse(animalInfo[2]), 0, double.Parse(animalInfo[3])),
                        "Hen" => new Hen(animalInfo[1], double.Parse(animalInfo[2]), 0, double.Parse(animalInfo[3])),
                        "Mouse" => new Mouse(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3]),
                        "Dog" => new Dog(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3]),
                        "Cat" => new Cat(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3], animalInfo[4]),
                        "Tiger" => new Tiger(animalInfo[1], double.Parse(animalInfo[2]), 0, animalInfo[3], animalInfo[4]),
                        _ => throw new ArgumentException("unknown animal type")
                    };
                    animals.Add(animal);
                }
                else
                {
                    string[] mealInfo = line.Split();
                    string type = mealInfo[0];
                    Food food = type switch
                    {
                        "Vegetable" => new Vegetable(int.Parse(mealInfo[1])),
                        "Fruit" => new Fruit(int.Parse(mealInfo[1])),
                        "Meat" => new Meat(int.Parse(mealInfo[1])),
                        "Seeds" => new Seeds(int.Parse(mealInfo[1])),
                        _ => throw new ArgumentException("unknown food type")
                    };
                    meals.Add(food);
                }
                i++;
            }
            foreach ((var animal, var food) in animals.Zip(meals))
            {
                animal.Sound();
                animal.EatFood(food);
            }
            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}