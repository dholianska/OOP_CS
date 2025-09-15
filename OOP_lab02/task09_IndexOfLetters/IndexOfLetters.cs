using System;
using System.Linq;

class IndexOfLetters
{
    static void Main()
    {
        char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f',
        'g','h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q',
        'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

        Console.WriteLine("enter your word: ");
        char[] array = Console.ReadLine().ToCharArray();

        for (int i=0; i<array.Length; i++)
        {
            for (int j=0; j<alphabet.Length; j++)
            {
                if (array[i] == alphabet[j])
                { 
                    Console.WriteLine($"{array[i]} -> {j}");
                    break;
                }
            }
        }
    }

}