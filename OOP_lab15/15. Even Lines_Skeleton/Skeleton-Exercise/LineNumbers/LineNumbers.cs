namespace LineNumbers
{
    using System;
    using System.Linq;
    using System.IO;

    public class LineNumbers
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt";
            string outputFilePath = @"..\..\..\output.txt";

            ProcessLines(inputFilePath, outputFilePath);
        }

        public static void ProcessLines(string inputFilePath, string outputFilePath)
        {
            string[] lines = File.ReadAllLines(inputFilePath);

            string[] result = lines
                .Select((line, index) =>
                {
                    int letters = line.Count(char.IsLetter);
                    int punctuation = line.Count(char.IsPunctuation);

                    return $"Line {index + 1}: {line} ({letters})({punctuation})";
                })
                .ToArray();

            File.WriteAllLines(outputFilePath, result);
        }
    }
}
