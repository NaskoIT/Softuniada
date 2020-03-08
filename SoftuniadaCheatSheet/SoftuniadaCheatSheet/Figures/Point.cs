using System;

namespace SoftuniadaCheatSheet.Figures
{
    public class Point
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

        public bool IsInsideTriangle(Triangle triangle)
        {
            double mainTriangleArea = CalculateArea(triangle.A, triangle.B, triangle.C);
            double firstSubTriangleArea = CalculateArea(triangle.A, triangle.B, this);
            double secondSubTriangleArea = CalculateArea(triangle.A, triangle.C, this);
            double thirdSubTriangleArea = CalculateArea(triangle.B, triangle.C, this);

            return Math.Abs(mainTriangleArea - (firstSubTriangleArea + secondSubTriangleArea + thirdSubTriangleArea)) <= 0.01;
        }

        private static double CalculateArea(Point a, Point b, Point c)
        {
            return Math.Abs((a.X * (b.Y - c.Y) +
                             b.X * (c.Y - a.Y) +
                             c.X * (a.Y - b.Y)) / 2.0);
        }
    }
}
