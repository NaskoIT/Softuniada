using System;
using System.Collections.Generic;
using System.Linq;

namespace PackagingFigures
{
    class Program
    {
        public static void Main()
        {
            var figures = ReadFigures();

            for (int i = 0; i < figures.Count; i++)
            {
                Figure firstFigure = figures[i];

                for (int j = 0; j < figures.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    Figure secondFigure = figures[j];
                    
                    if (firstFigure is Circle firstCircle)
                    {
                        //Check if second circle is inside first circle
                        if (secondFigure is Circle secondCircle)
                        {
                            if(secondCircle.Radius > firstCircle.Radius)
                            {
                                continue;
                            }

                            int distanceX = Math.Abs(firstCircle.CenterX - secondCircle.CenterX);
                            int distanceY = Math.Abs(firstCircle.CenterY - secondCircle.CenterY);
                            if(distanceX > firstCircle.Radius || distanceY > firstCircle.Radius)
                            {
                                continue;
                            }

                            long distanceBetweenCentersSquare = (long)distanceY * distanceY + (long)distanceX * distanceX;
                            long delatRSquare = (long)(firstCircle.Radius - secondCircle.Radius) * (firstCircle.Radius - secondCircle.Radius);
                        
                            if(distanceBetweenCentersSquare <= delatRSquare)
                            {
                                firstCircle.Children.Add(secondCircle);
                            }
                        }
                        else if(secondFigure is Rectangle rectangle)
                        {
                            //Check is rectangle is inside circle
                            int distanceX = Math.Max(firstCircle.CenterX - rectangle.LeftX, rectangle.RightX - firstCircle.CenterX);
                            int distanceY = Math.Max(rectangle.TopY - firstCircle.CenterY, firstCircle.CenterY - rectangle.BottomY);
                            long distance = ((long)distanceX * distanceX) + ((long)distanceY * distanceY);
                            bool isInside = (long)firstCircle.Radius * firstCircle.Radius >= distance;
                            if (isInside)
                            {
                                firstCircle.Children.Add(rectangle);
                            }
                        }
                    }
                    else if (firstFigure is Rectangle rectangle)
                    {
                        //Check if second circle is inside rectangle
                        if (secondFigure is Circle circle)
                        {
                            bool inLeft = circle.CenterX - rectangle.LeftX >= circle.Radius;
                            bool inRight = rectangle.RightX - circle.CenterX >= circle.Radius;
                            bool inTop = rectangle.TopY - circle.CenterY >= circle.Radius;
                            bool inBottom = circle.CenterY - rectangle.BottomY >= circle.Radius;

                            if(inLeft && inRight && inTop && inBottom)
                            {
                                rectangle.Children.Add(circle);
                            }
                        }
                        else if (secondFigure is Rectangle secondRectangle)
                        {
                            //Check if second rectangle is inside first rectangle
                            if(secondRectangle.RightX > rectangle.RightX || 
                               secondRectangle.LeftX < rectangle.LeftX ||
                               secondRectangle.TopY > rectangle.TopY ||
                               secondRectangle.BottomY < rectangle.BottomY)
                            {
                                continue;
                            }

                            rectangle.Children.Add(secondRectangle);
                        }
                    }
                }
            }

            for (int i = 0; i < figures.Count; i++)
            {
                Dfs(figures[i]);
            }

            Figure top = figures.OrderByDescending(f => f.Depth).ThenBy(x => x.Name).First();
            int depth = top.Depth;
            List<string> targetFigures = new List<string>();

            while(top != null && depth > 0)
            {
                targetFigures.Add(top.Name);
                depth -= 1;
                top = top.Successor;
            }

            Console.WriteLine(string.Join(" < ", targetFigures));
        }

        private static int Dfs(Figure figure)
        {
            if(figure.Depth > 0)
            {
                return figure.Depth;
            }

            figure.Depth = 1;
            figure.Successor = null;

            foreach (var child in figure.Children)
            {
                int currentDepth = Dfs(child) + 1;
                if(currentDepth > figure.Depth || 
                  (currentDepth == figure.Depth && 
                  child.Name.CompareTo(figure.Successor.Name) < 0))
                {
                    figure.Depth = currentDepth;
                    figure.Successor = child;
                }
            }

            return figure.Depth;
        }

        private static List<Figure> ReadFigures()
        {
            List<Figure> figures = new List<Figure>();
            string line = Console.ReadLine();

            while (line != "End")
            {
                string[] parameters = line.Split();
                string type = parameters[0];
                string name = parameters[1];

                int x1 = 0;
                int y1 = 0;
                int x2 = 0;
                int y2 = 0;
                Figure figure = null;

                switch (type)
                {
                    case "rectangle":
                        x1 = int.Parse(parameters[2]);
                        y1 = int.Parse(parameters[3]);
                        x2 = int.Parse(parameters[4]);
                        y2 = int.Parse(parameters[5]);
                        figure = new Rectangle(name, x1, y1, x2, y2);
                        break;
                    case "square":
                        x1 = int.Parse(parameters[2]);
                        y1 = int.Parse(parameters[3]);
                        int side = int.Parse(parameters[4]);
                        x2 = x1 + side;
                        y2 = y1 - side;
                        figure = new Rectangle(name, x1, y1, x2, y2);
                        break;
                    case "circle":
                        int centerX = int.Parse(parameters[2]);
                        int centerY = int.Parse(parameters[3]);
                        int radius = int.Parse(parameters[4]);
                        figure = new Circle(name, centerX, centerY, radius);
                        break;
                }

                figures.Add(figure);
                line = Console.ReadLine();
            }

            return figures;
        }
    }
}

public class Rectangle : Figure
{
    public Rectangle(string name, int leftX, int topY, int rightX, int bottomY) : base(name)
    {
        LeftX = leftX;
        TopY = topY;
        BottomY = bottomY;
        RightX = rightX;
    }

    public int LeftX { get; set; }

    public int TopY { get; set; }

    public int BottomY { get; set; }

    public int RightX { get; set; }
}

public class Circle : Figure
{
    public Circle(string name, int centerX, int centerY, int radius) : base(name)
    {
        CenterX = centerX;
        CenterY = centerY;
        Radius = radius;
    }

    public int CenterX { get; set; }

    public int CenterY { get; set; }

    public int Radius { get; set; }
}

public abstract class Figure
{
    public Figure(string name)
    {
        Name = name;
        Children = new List<Figure>();
    }

    public string Name { get; set; }

    public List<Figure> Children { get; private set; }

    public int Depth { get; set; }

    public Figure Successor { get; set; }
}
