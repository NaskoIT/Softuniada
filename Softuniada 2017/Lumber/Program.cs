using System;
using System.Collections.Generic;
using System.Linq;

namespace Lumber
{
    class Program
    {
        private static int logIdentifier = 1;
        private static readonly Dictionary<int, Log> logs = new Dictionary<int, Log>();
        private static List<int>[] graph;

        static void Main(string[] args)
        {
            int[] line = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int logsCount = line[0];
            int queriesCount = line[1];

            graph = new List<int>[logsCount + 1];
            for (int node = 1; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }

            BuildGraph(logsCount);
            List<HashSet<int>> connectedComponents = FindConnectedComponenets();

            for (int i = 0; i < queriesCount; i++)
            {
                int[] query = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int startLog = query[0];
                int destinationLog = query[1];

                bool hasPath = ExistsPath(startLog, destinationLog, connectedComponents);
                Console.WriteLine(hasPath ? "YES" : "NO");
            }
        }

        private static bool ExistsPath(int startLog, int destinationLog, List<HashSet<int>> connectedComponents)
        {
            foreach (var connectedComponent in connectedComponents)
            {
                if (connectedComponent.Contains(startLog) && connectedComponent.Contains(destinationLog))
                {
                    return true;
                }
            }

            return false;
        }

        private static List<HashSet<int>> FindConnectedComponenets()
        {
            List<HashSet<int>> connectedComponents = new List<HashSet<int>>();
            bool[] visited = new bool[graph.Length];

            for (int node = 1; node < graph.Length; node++)
            {
                if (!visited[node])
                {
                    HashSet<int> currentConnectedComponent = new HashSet<int>();
                    Dfs(node, visited, currentConnectedComponent);
                    connectedComponents.Add(currentConnectedComponent);
                }
            }

            return connectedComponents;
        }

        private static void Dfs(int node, bool[] visited, HashSet<int> currentConnectedComponent)
        {
            if (!visited[node])
            {
                visited[node] = true;

                foreach (var childNode in graph[node])
                {
                    Dfs(childNode, visited, currentConnectedComponent);
                }

                currentConnectedComponent.Add(node);
            }
        }

        private static void BuildGraph(int logsCount)
        {
            for (int i = 0; i < logsCount; i++)
            {
                int[] logCoordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
                Log log = new Log
                {
                    Id = logIdentifier++,
                    TopLeftX = logCoordinates[0],
                    TopLeftY = logCoordinates[1],
                    BottomRightX = logCoordinates[2],
                    BottomRightY = logCoordinates[3]
                };

                AddLogToGraph(log);
                logs.Add(log.Id, log);
            }
        }

        private static void AddLogToGraph(Log log)
        {
            foreach (Log otherLog in logs.Values)
            {
                if (log.IntercestWith(otherLog))
                {
                    graph[log.Id].Add(otherLog.Id);
                    graph[otherLog.Id].Add(log.Id);
                }
            }
        }
    }

    public class Log
    {
        public int Id { get; set; }

        public int TopLeftX { get; set; }

        public int TopLeftY { get; set; }

        public int BottomRightX { get; set; }

        public int BottomRightY { get; set; }

        public bool IntercestWith(Log log)
        {
            return this.TopLeftX <= log.BottomRightX &&
                log.TopLeftX <= this.BottomRightX &&
                this.TopLeftY >= log.BottomRightY &&
                log.TopLeftY >= this.BottomRightY;
        }
    }
}
