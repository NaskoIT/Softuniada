using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingZones
{
    class Program
    {
        private static Zone[] zones;
        private static Point targetPoint;
        private static readonly List<Point> freeSpaces = new List<Point>();

        static void Main(string[] args)
        {
            int parkingZonesCount = int.Parse(Console.ReadLine());
            ReadZones(parkingZonesCount);
            ReadPoints();
            ReadTargetPoint();
            int neededSecondsToTraverseBlock = int.Parse(Console.ReadLine());

            FindBestParkingSpace(neededSecondsToTraverseBlock);
        }

        private static void FindBestParkingSpace(int neededSecondsToTraverseBlock)
        {
            Point bestFreeSpace = null;
            double bestPrice = double.MaxValue;
            int bestTimeInSeconds = int.MaxValue;

            foreach (Point freeSpace in freeSpaces)
            {
                int distance = Math.Abs(freeSpace.X - targetPoint.X) + Math.Abs(freeSpace.Y - targetPoint.Y) - 1;
                distance *= 2;
                int seconds = distance * neededSecondsToTraverseBlock;
                int minutes = (int)Math.Ceiling(seconds / 60.0);
                double totalPrice = minutes * freeSpace.Zone.PricePerMinute;

                if (totalPrice < bestPrice || (totalPrice == bestPrice && bestTimeInSeconds > seconds))
                {
                    bestPrice = totalPrice;
                    bestTimeInSeconds = seconds;
                    bestFreeSpace = freeSpace;
                }
            }

            Console.WriteLine($"Zone Type: {bestFreeSpace.Zone.Name}; X: {bestFreeSpace.X}; Y: {bestFreeSpace.Y}; Price: {bestPrice:F2}");
        }

        private static void ReadTargetPoint()
        {
            int[] pointParts = Console.ReadLine().Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            targetPoint = new Point(pointParts[0], pointParts[1]);
        }

        private static void ReadPoints()
        {
            int[] tokens = Console.ReadLine().Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for (int i = 0; i < tokens.Length - 1; i += 2)
            {
                int x = tokens[i];
                int y = tokens[i + 1];
                Point point = new Point(x, y);
                Zone pointZone = GetPointZone(point);
                point.Zone = pointZone;
                freeSpaces.Add(point);
            }
        }

        private static Zone GetPointZone(Point point)
        {
            foreach (var zone in zones)
            {
                if (zone.ContainsPoint(point))
                {
                    return zone;
                }
            }

            return null;
        }

        private static void ReadZones(int parkingZonesCount)
        {
            zones = new Zone[parkingZonesCount];

            for (int i = 0; i < parkingZonesCount; i++)
            {
                string[] toknes = Console.ReadLine().Split(new char[] { ':', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                Point topLeftPoint = new Point(int.Parse(toknes[1]), int.Parse(toknes[2]));
                Zone zone = new Zone
                {
                    Name = toknes[0],
                    TopLeftPoint = topLeftPoint,
                    Width = int.Parse(toknes[3]),
                    Height = int.Parse(toknes[4]),
                    PricePerMinute = double.Parse(toknes[5])
                };

                zones[i] = zone;
            }
        }
    }

    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public Zone Zone { get; set; }
    }

    public class Zone
    {
        public string Name { get; set; }

        public Point TopLeftPoint { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public double PricePerMinute { get; set; }

        public bool ContainsPoint(Point point)
        {
            return point.X >= TopLeftPoint.X &&
                   point.X < TopLeftPoint.X + Width &&
                   point.Y >= TopLeftPoint.Y &&
                   point.Y < TopLeftPoint.Y + Height;
        }
    }
}
