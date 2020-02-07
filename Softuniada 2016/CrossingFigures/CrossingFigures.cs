using System;
using System.Text;

namespace CrossingFigures
{
    class CrossingFigures
    {
        public const double Epsilon = 0.01;

        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            int testCases = int.Parse(Console.ReadLine());

            for (int i = 0; i < testCases; i++)
            {
                string[] firstFigureParts = Console.ReadLine().Split(new[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                string[] secondFigureParts = Console.ReadLine().Split(new[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                Circle circle = null;
                Rectangle rectangle = null;

                if (firstFigureParts[0] == "circle")
                {
                    circle = ParseCircle(firstFigureParts);
                    rectangle = ParseRectangle(secondFigureParts);
                }
                else
                {
                    circle = ParseCircle(secondFigureParts);
                    rectangle = ParseRectangle(firstFigureParts);
                }

                int rectanglePointsInsideCircle = 0;
                foreach (var vertex in rectangle.Vertices)
                {
                    if (circle.IsPointInside(vertex))
                    {
                        rectanglePointsInsideCircle++;
                    }
                }

                int circlePointsInsideRectangle = 0;
                foreach (var vertex in circle.Vertices)
                {
                    if (rectangle.IsInside(vertex))
                    {
                        circlePointsInsideRectangle++;
                    }
                }

                if(circlePointsInsideRectangle == circle.Vertices.Length)
                {
                    sb.AppendLine("Circle inside rectangle");
                }
                else if (rectanglePointsInsideCircle == rectangle.Vertices.Length)
                {
                    sb.AppendLine("Rectangle inside circle");
                }
                else if (circlePointsInsideRectangle > 0 || rectanglePointsInsideCircle > 0)
                {
                    sb.AppendLine("Rectangle and circle cross");
                }
                else
                {
                    sb.AppendLine("Rectangle and circle do not cross");
                }
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static Rectangle ParseRectangle(string[] figureParts)
        {
            double topLeftX = double.Parse(figureParts[1]);
            double topLeftY = double.Parse(figureParts[2]);
            double bottomRightX = double.Parse(figureParts[3]);
            double bottomRightY = double.Parse(figureParts[4]);
            return new Rectangle(topLeftX, topLeftY, bottomRightX, bottomRightY);
        }

        private static Circle ParseCircle(string[] figureParts)
        {
            double x = double.Parse(figureParts[1]);
            double y = double.Parse(figureParts[2]);
            return new Circle(double.Parse(figureParts[3]), new Point(x, y));
        }
    }

    public class Circle
    {
        public Circle(double radius, Point center)
        {
            Radius = radius;
            Center = center;
            Top = new Point(Center.X, Center.Y + Radius);
            Bottom = new Point(Center.X, Center.Y - Radius);
            Left = new Point(Center.X - radius, Center.Y);
            Right = new Point(Center.X + radius, Center.Y);
            Vertices = new Point[] { Top, Right, Bottom, Left };
        }

        public double Radius { get; set; }

        public Point Top { get; set; }

        public Point Bottom { get; set; }

        public Point Left { get; set; }

        public Point Right { get; set; }

        public Point Center { get; set; }

        public Point[] Vertices { get; set; }

        public bool IsPointInside(Point point)
        {
            double distanceToCenter = point.CalculateDistance(Center);
            return distanceToCenter - Radius <= CrossingFigures.Epsilon;
        }
    }

    public class Rectangle
    {
        public Rectangle(double topLeftX, double topLeftY, double bottomRightX, double bottomRightY)
        {
            TopLeft = new Point(topLeftX, topLeftY);
            BottomRight = new Point(bottomRightX, bottomRightY);
            Width = bottomRightX - topLeftX;
            Height = topLeftY - bottomRightY;
            BottomLeft = new Point(topLeftX, topLeftY - Height);
            TopRight = new Point(topLeftX + Width, topLeftY);
            Vertices = new Point[] { TopLeft, TopRight, BottomLeft, BottomRight };
        }

        public Point TopLeft { get; set; }

        public Point BottomLeft { get; set; }

        public Point TopRight { get; set; }

        public Point BottomRight { get; set; }

        public double Width { get; private set; }

        public double Height { get; private set; }

        public Point[] Vertices { get; private set; }

        public bool IntercestWith(Rectangle rectangle)
        {
            return this.TopLeft.X <= rectangle.BottomRight.X &&
                rectangle.TopLeft.X <= this.BottomRight.X &&
                this.TopLeft.Y >= rectangle.BottomRight.Y &&
                rectangle.TopLeft.Y >= this.BottomRight.Y;
        }

        public bool IsInside(Rectangle rectangle)
        {
            return rectangle.TopLeft.X <= TopLeft.X &&
                   rectangle.TopLeft.Y >= TopLeft.Y &&
                   rectangle.BottomRight.X >= BottomRight.X &&
                   rectangle.BottomRight.Y <= BottomRight.Y;
        }

        public bool IsInside(Point point)
        {
            return this.TopLeft.X <= point.X && point.X <= this.BottomRight.X &&
                this.BottomRight.Y <= point.Y && point.Y <= TopLeft.Y;
        }
    }

    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double CalculateDistance(Point other)
        {
            double firstSide = Math.Abs(X - other.X);
            double secondSide = Math.Abs(Y - other.Y);
            return Math.Sqrt(firstSide * firstSide + secondSide * secondSide);
        }
    }
}
