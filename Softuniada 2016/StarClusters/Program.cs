using System;
using System.Collections.Generic;
using System.Linq;

namespace Softuniada2019
{
    class Program
    {
        private static List<Cluster> clusters = new List<Cluster>();
        private static List<Star> stars = new List<Star>();

        static void Main(string[] args)
        {
            int clustersCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < clustersCount; i++)
            {
                var tokens = Console.ReadLine().Split(new char[] { ' ', '(', ')', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[0];
                int x = int.Parse(tokens[1]);
                int y = int.Parse(tokens[2]);
                Cluster cluster = new Cluster(name, x, y);
                clusters.Add(cluster);
                stars.Add(new Star(x, y));
            }

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
                int[] tokens = command
                    .Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                for (int i = 0; i < tokens.Length; i += 2)
                {
                    var star = new Star(tokens[i], tokens[i + 1]);
                    stars.Add(star);
                }
            }

            FindClustersCenters();

            foreach (var cluster in clusters.OrderBy(x => x.Name))
            {
                Console.WriteLine($"{cluster.Name} ({Math.Round(cluster.X)}, {Math.Round(cluster.Y)}) -> {cluster.StarsCount} stars");
            }
        }

        private static void FindClustersCenters()
        {
            var distances = new double[stars.Count, clusters.Count];
            bool foundFinalClusters = false;

            while (!foundFinalClusters)
            {
                for (int star = 0; star < stars.Count; star++)
                {
                    for (int cluster = 0; cluster < clusters.Count; cluster++)
                    {
                        distances[star, cluster] = clusters[cluster].Distance(stars[star]);
                    }
                }

                foundFinalClusters = true;

                for (int star = 0; star < stars.Count; star++)
                {
                    double minDistance = double.MaxValue;
                    int minCluster = 0;

                    for (int cluster = 0; cluster < clusters.Count; cluster++)
                    {
                        if (minDistance > distances[star, cluster])
                        {
                            minDistance = distances[star, cluster];
                            minCluster = cluster;
                        }
                    }

                    if (stars[star].Cluster != minCluster)
                    {
                        stars[star].Cluster = minCluster;
                        foundFinalClusters = false;
                    }
                }

                for (int cluster = 0; cluster < clusters.Count; cluster++)
                {
                    var starsInCluster = stars.Where(s => s.Cluster == cluster).ToList();
                    clusters[cluster].X = starsInCluster.Average(s => s.X);
                    clusters[cluster].Y = starsInCluster.Average(s => s.Y);
                    clusters[cluster].StarsCount = starsInCluster.Count;
                }
            }
        }
    }


    public class Star : Point
    {
        public Star(double x, double y) : base(x, y)
        {
        }

        public int Cluster { get; set; }
    }

    public class Cluster : Point
    {
        public Cluster(string name, int x, int y) : base(x, y)
        {
            Name = name;
        }

        public string Name { get; set; }

        public int StarsCount { get; set; }
    }

    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Distance(Point point)
        {
            double width = point.X - this.X;
            double height = point.Y - this.Y;
            return width * width + height * height;
        }
    }
}
