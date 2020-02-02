using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mafia
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder();
            int groupNumber = 1;
            int[,] graph = null;
            bool isRunning = true;

            while (isRunning)
            {
                while (isRunning)
                {
                    string input = Console.ReadLine();
                    if (input == "end")
                    {
                        int maxFlow = Dinic(graph);
                        result.AppendLine($"Group {groupNumber++}: {maxFlow}");
                        isRunning = false;
                        break;
                    }

                    if (int.TryParse(input, out int computersCount))
                    {
                        if (graph != null)
                        {
                            int maxFlow = Dinic(graph);
                            result.AppendLine($"Group {groupNumber++}: {maxFlow}");
                        }

                        graph = new int[computersCount + 1, computersCount + 1];
                    }
                    else
                    {
                        int[] tokens = input
                            .Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();
                        int startNode = tokens[0];
                        int endNode = tokens[1];
                        int capacity = tokens[2];
                        graph[startNode, endNode] = capacity;
                        graph[endNode, startNode] = capacity;
                    }
                }
            }

            Console.WriteLine(result.ToString().Trim());
        }

        static int Dinic(int[,] graph)
        {
            int source = 1;
            int target = graph.GetLength(1) - 1;
            int maxFlow = 0;
            int[] childCounter = new int[graph.GetLength(1)];
            int[] bfsDistances = new int[graph.GetLength(1)];

            while (Bfs(source, target, bfsDistances, graph))
            {
                for (int i = 0; i < childCounter.Length; i++)
                {
                    childCounter[i] = 1;
                }

                int pathFlow;

                do
                {
                    pathFlow = Dfs(source, int.MaxValue, target, childCounter, bfsDistances, graph);
                    maxFlow += pathFlow;
                } 
                while (pathFlow != 0);
            }

            return maxFlow;
        }

        static int Dfs(int source, int flow, int end, int[] childCounter, int[] bfsDistance, int[,] graph)
        {
            if (source == end)
            {
                return flow;
            }
            for (int i = childCounter[source]; i < graph.GetLength(1); i++, childCounter[source]++)
            {
                var child = i;
                if (graph[source, child] > 0)
                {
                    if (bfsDistance[child] == bfsDistance[source] + 1)
                    {

                        int currentFlow = Dfs(child, Math.Min(flow, graph[source, child]), end, childCounter, bfsDistance, graph);
                        if (currentFlow > 0)
                        {
                            graph[source, child] -= currentFlow;
                            graph[child, source] += currentFlow;
                            return currentFlow;
                        }
                    }
                }
            }

            return 0;
        }

        static bool Bfs(int start, int end, int[] bfsDistances, int[,] graph)
        {
            for (int i = 0; i < bfsDistances.Length; i++)
            {
                bfsDistances[i] = -1;
            }

            bfsDistances[start] = 0;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();

                for (int childNode = 1; childNode < graph.GetLength(1); childNode++)
                {
                    if (graph[currentNode, childNode] > 0 && bfsDistances[childNode] == -1)
                    {
                        bfsDistances[childNode] = bfsDistances[currentNode] + 1;
                        queue.Enqueue(childNode);
                    } 
                }
            }

            return bfsDistances[end] >= 0;
        }
    }
}
