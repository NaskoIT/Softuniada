using System;
using System.Linq;

namespace CircumscribedCircle
{
    class Program
    {
        static void Main(string[] args)
        {
            int shapesCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < shapesCount; i++)
            {
                Circle circle = ReadCircle();
                Triangle triangle = ReadTriangle();
               
                bool isCenterInsideTriangle = IsPointInsideTriangle(circle.Center, triangle);
                bool isCircumscribed = circle.IsCircumscribed(triangle);
                string circumscribed = isCircumscribed ? "circumscribed" : "not circumscribed";
                string position = isCenterInsideTriangle ? "inside" : "outside";

                Console.WriteLine($"The circle is {circumscribed} and the center is {position}.");
            }
        }

        private static Triangle ReadTriangle()
        {
            double[] tokens = Console.ReadLine().Substring(9).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            return new Triangle
            {
                A = new Point
                {
                    X = tokens[0],
                    Y = tokens[1]
                },
                B = new Point
                {
                    X = tokens[2],
                    Y = tokens[3]
                },
                C = new Point
                {
                    X = tokens[4],
                    Y = tokens[5]
                }
            };
        }

        private static Circle ReadCircle()
        {
            double[] tokens = Console.ReadLine().Substring(7).Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(double.Parse).ToArray();
            return new Circle
            {
                Center = new Point
                {
                    X = tokens[0],
                    Y = tokens[1]
                },
                Radius = tokens[2]
            };
        }

        private static double CalculateArea(Point a, Point b, Point c)
        {
                return Math.Abs((a.X * (b.Y - c.Y) +
                                 b.X * (c.Y - a.Y) +
                                 c.X * (a.Y - b.Y)) / 2.0);
        }

        private static bool IsPointInsideTriangle(Point point, Triangle triangle)
        {
            double mainTriangleArea = CalculateArea(triangle.A, triangle.B, triangle.C);
            double firstSubTriangleArea = CalculateArea(triangle.A, triangle.B, point);
            double secondSubTriangleArea = CalculateArea(triangle.A, triangle.C, point);
            double thirdSubTriangleArea = CalculateArea(triangle.B, triangle.C, point);

            return Math.Abs(mainTriangleArea - (firstSubTriangleArea + secondSubTriangleArea + thirdSubTriangleArea)) <= 0.01;
        }
    }

    public class Circle
    {
        public double Radius { get; set; }

        public Point Center { get; set; }

        public bool IsCircumscribed(Triangle triangle)
        {
            return Math.Abs(triangle.A.CalculateDistance(Center) - Radius) <= 0.01 &&
                   Math.Abs(triangle.B.CalculateDistance(Center) - Radius) <= 0.01 &&
                   Math.Abs(triangle.C.CalculateDistance(Center) - Radius) <= 0.01;
        }
    }

    public class Triangle
    {
        public Point A { get; set; }

        public Point B { get; set; }

        public Point C { get; set; }
    }

    public struct Point
    {
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
