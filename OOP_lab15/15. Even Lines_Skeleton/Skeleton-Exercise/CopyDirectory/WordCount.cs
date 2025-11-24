using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace WordCountTask
{
    public class WordCount
    {
        static void Main()
        {
            string wordsFile = @"..\..\..\words.txt";
            string textFile = @"..\..\..\text.txt";
            string actualFile = @"..\..\..\actualResult.txt";
            string expectedFile = @"..\..\..\expectedResult.txt";

            ProcessWordCount(wordsFile, textFile, actualFile, expectedFile);
        }

        public static void ProcessWordCount(string wordsFile, string textFile, string actualFile, string expectedFile)
        {
            var words = File.ReadAllLines(wordsFile)
                            .Select(w => w.ToLower())
                            .ToArray();

            var text = File.ReadAllText(textFile).ToLower();

            char[] separators = { ' ', '\n', '\r', '.', ',', '!', '?', '-', '\t' };

            var textWords = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var counts = new Dictionary<string, int>();

            foreach (var word in words)
                counts[word] = textWords.Count(w => w == word);

            var actual = words
                .Select(w => $"{w} - {counts[w]}")
                .ToArray();

            File.WriteAllLines(actualFile, actual);

            var expected = counts
                .OrderByDescending(x => x.Value)
                .Select(x => $"{x.Key} - {x.Value}")
                .ToArray();

            File.WriteAllLines(expectedFile, expected);
        }

    }
}
