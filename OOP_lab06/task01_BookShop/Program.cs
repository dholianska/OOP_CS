using System;
using System.Runtime.InteropServices;
using System.Text;

public class Book
{
    private string author;
    private string title;
    private decimal price;
    public Book(string author, string title, decimal price)
    {
        this.Author = author;
        this.Title = title;
        this.Price = price;
    }
    public string Author
    {
        get
        {
            return this.author;
        }
        set
        {
            if (value.Any(char.IsDigit))
            {
                throw new ArgumentException("author not valid!");
            }
            this.author = value;
        }
    }
    public string Title
    {
        get
        {
            return this.title;
        }
        set
        {
            if (value.Length < 3)
            {
                throw new ArgumentException("title not valid!");
            }
            this.title = value;
        }
    }
    public virtual decimal Price
    {
        get
        {
            return this.price;
        }
        set
        {
            if (value < 0 || value == 0)
            {
                throw new ArgumentException("price not valid!");
            }
            this.price = value;
        }
    }
    public override string ToString()
    {
        var resultBuilder = new StringBuilder();
        resultBuilder.AppendLine($"Type: {this.GetType().Name}")
            .AppendLine($"Title: {this.Title}")
            .AppendLine($"Author: {this.Author}")
            .AppendLine($"Price: {this.Price:f2}");
        string result = resultBuilder.ToString().TrimEnd();
        return result;
    }
}
public class GoldenEditionBook : Book
{
    public GoldenEditionBook(string author, string title, decimal price)
        : base(author, title, price)
    {

    }
    public override decimal Price
    {
        get { return base.Price * 1.3m; }
    }
}
class Program
{
    static void Main()
    {
        try
        {
            string author = Console.ReadLine();
            string title = Console.ReadLine();
            decimal price = decimal.Parse(Console.ReadLine());

            Book book = new Book(author, title, price);
            GoldenEditionBook goldenEditionBook = new GoldenEditionBook(author, title, price);

            Console.WriteLine(book + Environment.NewLine);
            Console.WriteLine(goldenEditionBook);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}