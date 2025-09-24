using System;
using System.Collections.Generic;
/* 4
DSL-10 280 B
V7-55 200 35
DSL-13 305 55 A+
V7-54 190 30 D
4
FordMondeo DSL-13 Purple
VolkswagenPolo V7-54 1200 Yellow
VolkswagenPassat DSL-10 1375 Blue
FordFusion DSL-13
*/

class Program
{
    class Engine
    {
        public string Model;
        public int Power;
        public string Displacement;
        public string Efficiency;

        public Engine(string model, int power, string displacement = "n/a", string efficiency = "n/a")
        {
            Model = model;
            Power = power;
            Displacement = displacement;
            Efficiency = efficiency;
        }
    }

    class Car
    {
        public string Model;
        public Engine Engine;
        public string Weight;
        public string Color;

        public Car(string model, Engine engine, string weight = "n/a", string color = "n/a")
        {
            Model = model;
            Engine = engine;
            Weight = weight;
            Color = color;
        }
    }

    static void Main()
    {
        
        int n = int.Parse(Console.ReadLine());
        List<Engine> engines = new List<Engine>();
        for (int i = 0; i < n; i++)
        {
            string[] input = Console.ReadLine().Split();
            string model = input[0];
            int power = int.Parse(input[1]);
            string displacement = "n/a";
            string efficiency = "n/a";

            if (input.Length == 3)
            {
                if (int.TryParse(input[2], out int d))
                    displacement = input[2];
                else
                    efficiency = input[2];
            }
            else if (input.Length == 4)
            {
                displacement = input[2];
                efficiency = input[3];
            }

            engines.Add(new Engine(model, power, displacement, efficiency));
        }

  
        int m = int.Parse(Console.ReadLine());
        List<Car> cars = new List<Car>();
        for (int i = 0; i < m; i++)
        {
            string[] input = Console.ReadLine().Split();
            string model = input[0];
            string engineModel = input[1];
            string weight = "n/a";
            string color = "n/a";

            if (input.Length == 3)
            {
                if (int.TryParse(input[2], out int w))
                    weight = input[2];
                else
                    color = input[2];
            }
            else if (input.Length == 4)
            {
                weight = input[2];
                color = input[3];
            }

            
            Engine engine = null;
            foreach (var e in engines)
            {
                if (e.Model == engineModel)
                {
                    engine = e;
                    break;
                }
            }

            cars.Add(new Car(model, engine, weight, color));
        }

        
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Model}:");
            Console.WriteLine($" {car.Engine.Model}:");
            Console.WriteLine($" Power: {car.Engine.Power}");
            Console.WriteLine($" Displacement: {car.Engine.Displacement}");
            Console.WriteLine($" Efficiency: {car.Engine.Efficiency}");
            Console.WriteLine($" Weight: {car.Weight} Color: {car.Color}");
        }
    }
}
