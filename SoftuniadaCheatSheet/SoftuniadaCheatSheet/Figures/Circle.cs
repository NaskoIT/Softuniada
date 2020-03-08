using System;

namespace SoftuniadaCheatSheet.Figures
{
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
            return distanceToCenter - Radius <= 0.01;
        }

        public bool CrossWithRectangle(Rectangle rectangle)
        {
            int rectanglePointsInsideCircle = 0;
            foreach (var vertex in rectangle.Vertices)
            {
                if (IsPointInside(vertex))
                {
                    rectanglePointsInsideCircle++;
                }
            }

            int circlePointsInsideRectangle = 0;
            foreach (var vertex in Vertices)
            {
                if (rectangle.IsInside(vertex))
                {
                    circlePointsInsideRectangle++;
                }
            }

            return circlePointsInsideRectangle > 0 || rectanglePointsInsideCircle > 0;
        }

        public bool IsInsideRectangle(Rectangle rectangle)
        {
            int circlePointsInsideRectangle = 0;
            foreach (var vertex in Vertices)
            {
                if (rectangle.IsInside(vertex))
                {
                    circlePointsInsideRectangle++;
                }
            }

            return circlePointsInsideRectangle == Vertices.Length;
        }

        public bool IsInside(Rectangle rectangle)
        {
            double distanceX = Math.Max(Center.X - rectangle.TopLeft.X, rectangle.BottomRight.X - this.Center.X);
            double distanceY = Math.Max(rectangle.TopLeft.Y - this.Center.Y, this.Center.Y - rectangle.BottomRight.Y);
            double distance = distanceX * distanceX + distanceY * distanceY;
            bool isInside = this.Radius * this.Radius >= distance;

            return isInside;
        }

        public bool IsInside(Circle circle)
        {
            if (Radius > circle.Radius)
            {
                return false;
            }

            double distanceX = Math.Abs(circle.Center.X - this.Center.X);
            double distanceY = Math.Abs(circle.Center.Y - this.Center.Y);
            if (distanceX > circle.Radius || distanceY > circle.Radius)
            {
                return false;
            }

            double distanceBetweenCentersSquare = distanceY * distanceY + distanceX * distanceX;
            double delatRSquare = (circle.Radius - this.Radius) * (circle.Radius - this.Radius);

            if (distanceBetweenCentersSquare <= delatRSquare)
            {
                return true;
            }

            return false;
        }

        public static Circle Parse(string[] figureParts)
        {
            double x = double.Parse(figureParts[1]);
            double y = double.Parse(figureParts[2]);
            return new Circle(double.Parse(figureParts[3]), new Point(x, y));
        }

        public bool IsCircumscribed(Triangle triangle)
        {
            return Math.Abs(triangle.A.CalculateDistance(Center) - Radius) <= 0.01 &&
                   Math.Abs(triangle.B.CalculateDistance(Center) - Radius) <= 0.01 &&
                   Math.Abs(triangle.C.CalculateDistance(Center) - Radius) <= 0.01;
        }
    }
}
