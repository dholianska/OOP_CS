using System;

public abstract class Food
{
    public int HappinessPoints { get; }

    protected Food(int happinessPoints)
    {
        HappinessPoints = happinessPoints;
    }
}
public class Cram : Food { public Cram() : base(2) { } }
public class Lembas : Food { public Lembas() : base(3) { } }
public class Apple : Food { public Apple() : base(1) { } }
public class Melon : Food { public Melon() : base(1) { } }
public class HoneyCake : Food { public HoneyCake() : base(5) { } }
public class Mushrooms : Food { public Mushrooms() : base(-10) { } }
public class Other : Food { public Other() : base(-1) { } }

public class FoodFactory
{
    public Food GetFood(string foodName)
    {
        foodName = foodName.ToLower();

        return foodName switch
        {
            "cram" => new Cram(),
            "lembas" => new Lembas(),
            "apple" => new Apple(),
            "melon" => new Melon(),
            "honeycake" => new HoneyCake(),
            "mushrooms" => new Mushrooms(),
            _ => new Other()
        };
    }
}

public abstract class Mood
{
    public string Name { get; }

    protected Mood(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name;
    }
}
public class Angry : Mood { public Angry() : base("Angry") { } }
public class Sad : Mood { public Sad() : base("Sad") { } }
public class Happy : Mood { public Happy() : base("Happy") { } }
public class JavaScript : Mood { public JavaScript() : base("JavaScript") { } }

public class MoodFactory
{
    public Mood GetMood(int happiness)
    {
        if (happiness < -5) return new Angry();
        if (happiness <= 0) return new Sad();
        if (happiness <= 15) return new Happy();
        return new JavaScript();
    }
}

public class Gandalf
{
    public int Happiness { get; private set; }

    private readonly FoodFactory _foodFactory = new();
    private readonly MoodFactory _moodFactory = new();

    public void Eat(string[] foods)
    {
        foreach (var foodName in foods)
        {
            var food = _foodFactory.GetFood(foodName);
            Happiness += food.HappinessPoints;
        }
    }

    public Mood GetMood() => _moodFactory.GetMood(Happiness);
}

class Program
{
    static void Main()
    {
        string[] foods = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        Gandalf gandalf = new Gandalf();
        gandalf.Eat(foods);

        Console.WriteLine(gandalf.Happiness);
        Console.WriteLine(gandalf.GetMood());
    }
}



