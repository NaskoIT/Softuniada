using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace MerchantsOfNovigrad
{
    class Program
    {
        private const int White = 0;
        private const int Grey = 1;
        private const int Black = 2;

        private static List<int>[] graph;
        private static List<int>[] reversedGraph;
        private static int[] visited;
        private static bool[] cycles;
        private static bool hasCycle = false;
        private static BigInteger[] pathsToNode;

        static void Main(string[] args)
        {
            int[] graphInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int townsCount = graphInfo[0];
            int connectionsCount = graphInfo[1];

            visited = new int[townsCount + 1];
            cycles = new bool[townsCount + 1];
            graph = new List<int>[townsCount + 1];
            reversedGraph = new List<int>[townsCount + 1];
            for (int i = 1; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
                reversedGraph[i] = new List<int>();
            }

            for (int i = 0; i < connectionsCount; i++)
            {
                int[] tokens = Console.ReadLine().Split().Select(int.Parse).ToArray();
                int parentNode = tokens[0];
                int childNode = tokens[1];
                graph[parentNode].Add(childNode);
                reversedGraph[childNode].Add(parentNode);
            }

            FindCycles(graph, 1);
            ResetVisited();
            Dfs(reversedGraph, reversedGraph.Length - 1);

            for (int node = 0; node < cycles.Length; node++)
            {
                if (cycles[node] && visited[node] == Black)
                {
                    hasCycle = true;
                    break;
                }
            }

            if (hasCycle)
            {
                Console.WriteLine("infinite");
            }
            else
            {
                pathsToNode = new BigInteger[townsCount + 1];
                pathsToNode[1] = 1;
                pathsToNode[graph.Length - 1] = 0;
                ResetVisited();
                FindPaths(graph.Length - 1);

                Console.WriteLine($"{pathsToNode[graph.Length - 1] % (int)Math.Pow(10, 9)} {(cycles.Any(c => c) ? "yes" : "no")}");
            }
        }

        private static void FindPaths(int node)
        {
            visited[node] = Black;

            foreach (var parent in reversedGraph[node])
            {
                if (visited[parent] == White)
                {
                    FindPaths(parent);
                }

                pathsToNode[node] += pathsToNode[parent];
            }
        }

        private static void Dfs(List<int>[] reversedGraph, int node)
        {
            visited[node] = Black;

            foreach (var child in reversedGraph[node])
            {
                if (visited[child] == White)
                {
                    Dfs(reversedGraph, child);
                }
            }
        }

        private static void ResetVisited()
        {
            for (int i = 0; i < visited.Length; i++)
            {
                visited[i] = White;
            }
        }

        private static void FindCycles(List<int>[] graph, int node)
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
    }
}
