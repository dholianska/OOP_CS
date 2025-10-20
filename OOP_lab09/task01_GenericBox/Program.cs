using System;

public class Box<T>
{
    public T Value { get; }

    public Box(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{typeof(T).FullName}: {Value}";
    }
}

public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string input = Console.ReadLine();

            if (int.TryParse(input, out int number))
            {
                var box = new Box<int>(number);
                Console.WriteLine(box);
            }
            else
            {
                var box = new Box<string>(input);
                Console.WriteLine(box);
            }
        }
    }
}
