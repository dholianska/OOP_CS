namespace DirectoryTraversal
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    //..\..\..\..
    public class DirectoryTraversal
    {
        static void Main()
        {
            string path = Console.ReadLine();
            string reportFileName = @"\report.txt";

            string reportContent = TraverseDirectory(path);
            Console.WriteLine(reportContent);

            WriteReportToDesktop(reportContent, reportFileName);
        }

        public static string TraverseDirectory(string inputFolderPath)
        {
            var files = Directory.GetFiles(inputFolderPath);

            var fileInfo = files
                .Select(f => new FileInfo(f))
                .GroupBy(f => f.Extension.ToLower()) 
                .OrderByDescending(g => g.Count())   
                .ThenBy(g => g.Key);                 

            StringBuilder sb = new StringBuilder();

            foreach (var group in fileInfo)
            {
                sb.AppendLine(group.Key);

                foreach (var file in group.OrderBy(f => f.Length))
                {
                    double sizeKb = file.Length / 1024.0;
                    sb.AppendLine($"--{file.Name} - {sizeKb:F3}kb");
                }
            }

            return sb.ToString();
        }

        public static void WriteReportToDesktop(string textContent, string reportFileName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            string fullOutputPath = desktopPath + reportFileName;

            File.WriteAllText(fullOutputPath, textContent);
        }
    }
}
