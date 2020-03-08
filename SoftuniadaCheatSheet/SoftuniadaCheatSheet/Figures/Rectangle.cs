namespace SoftuniadaCheatSheet.Figures
{
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

        public bool IsInsideCircle(Circle circle)
        {
            int rectanglePointsInsideCircle = 0;
            foreach (var vertex in Vertices)
            {
                if (circle.IsPointInside(vertex))
                {
                    rectanglePointsInsideCircle++;
                }
            }

            return rectanglePointsInsideCircle == Vertices.Length;
        }

        public bool IsInside(Circle circle)
        {
            bool inLeft = circle.Center.X - TopLeft.X >= circle.Radius;
            bool inRight = BottomRight.X - circle.Center.X >= circle.Radius;
            bool inTop = TopLeft.Y - circle.Center.Y >= circle.Radius;
            bool inBottom = circle.Center.Y - BottomRight.Y >= circle.Radius;

            return inLeft && inRight && inTop && inBottom;
        }

        public static Rectangle Parse(string[] figureParts)
        {
            double topLeftX = double.Parse(figureParts[1]);
            double topLeftY = double.Parse(figureParts[2]);
            double bottomRightX = double.Parse(figureParts[3]);
            double bottomRightY = double.Parse(figureParts[4]);
            return new Rectangle(topLeftX, topLeftY, bottomRightX, bottomRightY);
        }
    }
}
