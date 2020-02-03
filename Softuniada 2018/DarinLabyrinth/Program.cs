using System;
using System.Collections.Generic;
using System.Linq;

namespace DarinLabyrinth
{
    class Program
    {
        private static List<int>[] graph;
        private static int[] depths;
        private static int[] lowpoints;
        private static bool[] visited;
        private static int?[] parent;
        private static HashSet<int> articulationPoints;

        static void Main(string[] args)
        {
            int numberOfRooms = int.Parse(Console.ReadLine());
            int numberOfTunels = int.Parse(Console.ReadLine());

            graph = new List<int>[numberOfRooms];

            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            for (int i = 0; i < numberOfTunels; i++)
            {
                int[] connection = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int startNode = connection[0];
                int endNode = connection[1];
                graph[startNode].Add(endNode);
                graph[endNode].Add(startNode);
            }

            var path = FindPath(0, graph.Length - 1);
            FindArticulationPoints();

            var keyRooms = path.Where(room => articulationPoints.Contains(room) && FindPath(0, graph.Length - 1, room) == null);
            long sum = 0;
            foreach (var room in keyRooms)
            {
                sum += room;
            }
            Console.WriteLine(sum);
            Console.WriteLine(string.Join("->", keyRooms));
        }

        public static void FindArticulationPoints()
        {
            depths = new int[graph.Length];
            lowpoints = new int[graph.Length];
            visited = new bool[graph.Length];
            parent = new int?[graph.Length];
            articulationPoints = new HashSet<int>();

            if (graph.Length > 0)
            {
                FindArticulationPoints(0, 1);
            }
        }

        private static void FindArticulationPoints(int node, int depth)
        {
            visited[node] = true;
            lowpoints[node] = depth;
            depths[node] = depth;
            int childCount = 0;
            bool isArticulation = false;

            foreach (var childNode in graph[node])
            {
                if (!visited[childNode])
                {
                    parent[childNode] = node;
                    FindArticulationPoints(childNode, depth + 1);
                    childCount += 1;
                    if (lowpoints[childNode] >= depths[node])
                    {
                        isArticulation = true;
                    }
                    lowpoints[node] = Math.Min(lowpoints[node], lowpoints[childNode]);
                }
                else if (childNode != parent[node])
                {
                    lowpoints[node] = Math.Min(lowpoints[node], depths[childNode]);
                }
            }

            if ((parent[node] != null && isArticulation) || (parent[node] == null && childCount > 1))
            {
                articulationPoints.Add(node);
            }
        }

        private static IEnumerable<int> FindPath(int sourceNode, int destinationNode, int keyNode = -1)
        {
            int[] parents = new int[graph.Length];
            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = -1;
            }

            bool[] visited = new bool[parents.Length];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(sourceNode);
            visited[sourceNode] = true;

            while (queue.Count > 0)
            {
                int node = queue.Dequeue();
                if (node == keyNode)
                {
                    continue;
                }
                if (node == destinationNode)
                {
                    break;
                }

                foreach (var child in graph[node])
                {
                    if (!visited[child])
                    {
                        visited[child] = true;
                        queue.Enqueue(child);
                        parents[child] = node;
                    }
                }
            }

            if (parents[destinationNode] == -1)
            {
                return null;
            }

            Stack<int> path = new Stack<int>();
            int currentNode = destinationNode;

            while (parents[currentNode] != -1)
            {
                currentNode = parents[currentNode];
                if (currentNode == sourceNode)
                {
                    break;
                }

                path.Push(currentNode);
            }

            return path;
        }
    }
}

