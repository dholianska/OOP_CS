using System;

class Program
{
    class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    class Rectangle 
    {
        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public Rectangle(Point topLeft, Point bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public bool Contains(Point point)
        {
            if ( point.X >= TopLeft.X &&
                   point.X <= BottomRight.X &&
                   point.Y >= TopLeft.Y &&
                   point.Y <= BottomRight.Y)
            {  return true; }
            else { return false; }
        }
    }
    static void Main()
    {
        string[] rectInput = Console.ReadLine().Split();
        double x1 = double.Parse(rectInput[0]);
        double y1 = double.Parse(rectInput[1]);
        double x2 = double.Parse(rectInput[2]);
        double y2 = double.Parse(rectInput[3]);

        Rectangle rect = new Rectangle(new Point(x1, y1), new Point(x2, y2));

        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] pointInput = Console.ReadLine().Split();
            double px = double.Parse(pointInput[0]);
            double py = double.Parse(pointInput[1]);

            Point p = new Point(px, py);
            Console.WriteLine(rect.Contains(p));
        }
    }
}