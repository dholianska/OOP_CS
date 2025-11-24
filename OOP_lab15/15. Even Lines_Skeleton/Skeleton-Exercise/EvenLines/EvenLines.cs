namespace EvenLines
{
    using System;
    using System.IO;
    using System.Linq;
    using static System.Net.Mime.MediaTypeNames;

    public class EvenLines
    {
        static void Main()
        {
            string inputFilePath = @"..\..\..\text.txt"; 

            Console.WriteLine(ProcessLines(inputFilePath));
        }

        public static string ProcessLines(string inputFilePath)
        {
            var reader = new StreamReader(inputFilePath);
            int counter = 0;
            string line;
            var result = "";

            while ((line = reader.ReadLine()) != null) 
            {
                if (counter % 2 ==0)
                {
                    var replaced = line
                        .Replace('-', '@')
                        .Replace(',', '@')
                        .Replace('.', '@')
                        .Replace('!', '@')
                        .Replace('?', '@');

                    var reversed = string.Join(" ",
                    replaced.Split(' ', StringSplitOptions.RemoveEmptyEntries).Reverse()); 
                    result += reversed + Environment.NewLine;
                }
                counter++;
            }
            return result;
        }
    }
}
