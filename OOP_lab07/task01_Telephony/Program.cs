using System;
using System.Linq;

interface ICallable
{
    string Call(string number);
}
interface IBrowsable
{
    string Browse(string url);
}

public class Smartphone : ICallable, IBrowsable
{
    public string Call(string number)
    {
        if (!number.All(char.IsDigit))
        {
            return "invalid number!";
        }
        return $"calling... {number}";
    }
    public string Browse(string url)
    {
        if (url.Any(char.IsDigit))
        {
            return "invalid url!";
        }
        return $"browsing: {url}";
    }
}

class Program
{
    static void Main()
    {
        string[] inputNumbers = Console.ReadLine().Split();
        string[] inputURL = Console.ReadLine().Split();

        var smartphone = new Smartphone();

        foreach (var number in inputNumbers)
        {
            Console.WriteLine(smartphone.Call(number));
        }
        foreach (var url in inputURL)
        {
            Console.WriteLine(smartphone.Browse(url));
        }
    }
}