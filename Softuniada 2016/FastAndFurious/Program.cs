using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace FastAndFurious
{
    class Program
    {
        private static readonly List<Road> roads = new List<Road>();
        private static double[,] graph;
        private static readonly Dictionary<string, int> townsByIndex = new Dictionary<string, int>();
        private static int townsCount = 0;
        private static readonly Dictionary<string, SortedSet<Record>> driverByRecords = new Dictionary<string, SortedSet<Record>>();

        static void Main(string[] args)
        {
            ReadInput();

            BuildGraph();

            List<string> result = new List<string>();

            foreach (var driverByRecord in driverByRecords)
            {
                string driver = driverByRecord.Key;
                Record[] records = driverByRecord.Value.ToArray();
                bool isSpeeding = false;

                for (int currentIndex = 0; currentIndex < records.Length && !isSpeeding; currentIndex++)
                {
                    for (int nextIndex = currentIndex + 1; nextIndex < records.Length; nextIndex++)
                    {
                        string firstTown = records[currentIndex].CameraName;
                        string secondTown = records[nextIndex].CameraName;
                        TimeSpan actualTimeInHour = records[nextIndex].Time - records[currentIndex].Time;
                        double bestTimeInHours = DijkstraAlgorithm(graph, townsByIndex[firstTown], townsByIndex[secondTown]);

                        if (bestTimeInHours != double.MaxValue && bestTimeInHours > actualTimeInHour.TotalHours)
                        {
                            result.Add(driver);
                            isSpeeding = true;
                            break;
                        }
                    }
                }
            }

            foreach (var driver in result.OrderBy(x => x))
            {
                Console.WriteLine(driver);
            }
        }

        private static void BuildGraph()
        {
            graph = new double[townsCount, townsCount];

            foreach (var road in roads)
            {
                int firstTownIndex = townsByIndex[road.FirstCameraName];
                int secondTownIndex = townsByIndex[road.SecondCameraName];
                graph[firstTownIndex, secondTownIndex] = road.TimeInHours;
                graph[secondTownIndex, firstTownIndex] = road.TimeInHours;
            }
        }

        public static double DijkstraAlgorithm(double[,] graph, int sourceNode, int destinationNode)
        {
            int elementsCount = graph.GetLength(0);
            double[] distances = new double[elementsCount];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = double.MaxValue;
            }

            distances[sourceNode] = 0;

            bool[] used = new bool[elementsCount];
            int?[] previous = new int?[elementsCount];

            while (true)
            {
                double minDistance = double.MaxValue;
                int minNode = 0;

                for (int node = 0; node < elementsCount; node++)
                {
                    if (!used[node] && distances[node] < minDistance)
                    {
                        minDistance = distances[node];
                        minNode = node;
                    }
                }

                if (minDistance == double.MaxValue)
                {
                    break;
                }

                used[minNode] = true;

                for (int node = 0; node < elementsCount; node++)
                {
                    double weight = graph[minNode, node];

                    if (weight > 0)
                    {
                        double currentMinDistance = distances[minNode] + weight;
                        if (currentMinDistance < distances[node])
                        {
                            distances[node] = currentMinDistance;
                            previous[node] = minNode;
                        }
                    }
                }
            }

            return distances[destinationNode];
        }

        private static void ReadInput()
        {
            string command = Console.ReadLine();

            while ((command = Console.ReadLine()) != "Records:")
            {
                string[] tokens = command.Split();
                Road road = new Road
                {
                    FirstCameraName = tokens[0],
                    SecondCameraName = tokens[1],
                    Distance = double.Parse(tokens[2]),
                    SpeedLimit = double.Parse(tokens[3])
                };

                roads.Add(road);
                AddTown(road.FirstCameraName);
                AddTown(road.SecondCameraName);
            }

            while ((command = Console.ReadLine()) != "End")
            {
                string[] tokens = command.Split();
                string licensePlateNumber = tokens[1];
                Record record = new Record
                {
                    CameraName = tokens[0],
                    Time = TimeSpan.Parse(tokens[2], CultureInfo.InvariantCulture)
                };

                if (!driverByRecords.ContainsKey(licensePlateNumber))
                {
                    driverByRecords[licensePlateNumber] = new SortedSet<Record>();
                }

                driverByRecords[licensePlateNumber].Add(record);
            }
        }

        private static void AddTown(string townName)
        {
            if (!townsByIndex.ContainsKey(townName))
            {
                townsByIndex.Add(townName, townsCount++);
            }
        }
    }

    public class Road
    {
        public string FirstCameraName { get; set; }

        public string SecondCameraName { get; set; }

        public double Distance { get; set; }

        public double SpeedLimit { get; set; }

        public double TimeInHours => Distance / SpeedLimit;
    }

    public class Record : IComparable<Record>
    {
        public string CameraName { get; set; }

        public TimeSpan Time { get; set; }

        public int CompareTo(Record other)
        {
            return Time.CompareTo(other.Time);
        }
    }
}
