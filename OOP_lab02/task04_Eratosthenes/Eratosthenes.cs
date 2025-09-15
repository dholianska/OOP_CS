using System;

class Eratosthenes
{
    static void Main()
    {
        Console.WriteLine("enter n to find all prime numbers in range [1…n]: ");
        int n = int.Parse(Console.ReadLine());

        bool[] primes = new bool[n + 1];
        for (int i = 0; i <= n; i++)
        {
            primes[i] = true;
        }
        primes[0] = false;
        primes[1] = false;

        for (int p = 2; p * p <= n; p++)
        {
            if (primes[p])
            {
                for (int i = p * p; i <= n; i += p)
                {
                    primes[i] = false;
                }
            }
        }

        Console.WriteLine($"prime numbers before {n}:");
        for (int i = 2; i <= n; i++)
        {
            if (primes[i])
            {
                Console.Write(i + " ");
            }
        }
    }
}