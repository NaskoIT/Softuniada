using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SoftuniadaCheatSheet.Graphs
{
    public static class Graph
    {
        private const int White = 0;
        private const int Grey = 1;
        private const int Black = 2;

        private static List<int>[] graph;

        private static List<int>[] reversedGraph;
        private static int[] visited;
        private static bool[] cycles;

        public static void InitializeGraph(int nodesCount)
        {
            visited = new int[nodesCount];
            graph = new List<int>[nodesCount];

            for (int node = 0; node < graph.Length; node++)
            {
                graph[node] = new List<int>();
            }
        }

        public static void InitializReversedGraph(int nodesCount)
        {
            cycles = new bool[nodesCount];
            reversedGraph = new List<int>[nodesCount];

            for (int node = 0; node < reversedGraph.Length; node++)
            {
                reversedGraph[node] = new List<int>();
            }
        }

        public static void ReadDirectedGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                int[] tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentNode = tokens[0];
                int childNode = tokens[1];
                graph[parentNode].Add(childNode);
                reversedGraph[childNode].Add(parentNode);
            }
        }

        public static void ReadUndirectedGraph(int edgesCount)
        {
            for (int i = 0; i < edgesCount; i++)
            {
                int[] tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentNode = tokens[0];
                int childNode = tokens[1];
                graph[parentNode].Add(childNode);
                graph[childNode].Add(parentNode);
            }
        }

        private static void Dfs(int node)
        {
            visited[node] = Black;

            foreach (var child in graph[node])
            {
                if (visited[child] == White)
                {
                    Dfs(child);
                }
            }
        }

        public static void ResetVisited()
        {
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = White;
            }
        }

        public static void FindCycles(List<int>[] graph, int node)
        {
            visited[node] = Grey;

            foreach (var child in graph[node])
            {
                if (visited[child] == White)
                {
                    FindCycles(graph, child);
                }
                else if (visited[child] == Grey)
                {
                    cycles[child] = true;
                }
            }

            visited[node] = Black;
        }

        public static BigInteger FindPaths(int node)
        {
            BigInteger[] pathsToNode = new BigInteger[graph.Length];
            for (int i = 0; i < pathsToNode.Length; i++)
            {
                pathsToNode[i] = BigInteger.Zero;
            }

            pathsToNode[0] = 1;
            ResetVisited();
            FindPaths(graph.Length - 1);
            return pathsToNode[node];
        }

        public static void FindPaths(int node, BigInteger[] pathsToNode)
        {
            visited[node] = Black;

            foreach (var parent in reversedGraph[node])
            {
                if (visited[parent] == White)
                {
                    FindPaths(parent, pathsToNode);
                }

                pathsToNode[node] += pathsToNode[parent];
            }
        }

        public static IEnumerable<int> FindPath(int sourceNode, int destinationNode, int keyNode = -1)
        {
            ResetVisited();
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
