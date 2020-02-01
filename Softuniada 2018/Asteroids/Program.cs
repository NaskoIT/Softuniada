using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "end")
            {
                string[] fieldDimensoins = command.Split('x');
                int n = int.Parse(fieldDimensoins[0]);

                char[][] matrix = new char[n][];
                for (int row = 0; row < n; row++)
                {
                    matrix[row] = Console.ReadLine().ToCharArray();
                }

                SortedDictionary<int, int> areasByCount = FindAsteroids(matrix);

                foreach (var areaByCount in areasByCount)
                {
                    sb.AppendLine($"{areaByCount.Value}x{areaByCount.Key}");
                }

                sb.AppendLine($"Total: {areasByCount.Values.Sum()}");
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

        private static SortedDictionary<int, int> FindAsteroids(char[][] matrix)
        {
            bool[][] visited = new bool[matrix.Length][];
            for (int row = 0; row < visited.Length; row++)
            {
                visited[row] = new bool[matrix[row].Length];
            }

            SortedDictionary<int, int> areasByCount = new SortedDictionary<int, int>(Comparer<int>.Create((x, y) => y - x));

            for (int row = 0; row < matrix.Length; row++)
            {
                for (int col = 0; col < matrix[row].Length; col++)
                {
                    if (matrix[row][col] == '1' && !visited[row][col])
                    {
                        int area = CalculateAsteroidsArea(row, col, matrix, visited);
                        if (!areasByCount.ContainsKey(area))
                        {
                            areasByCount[area] = 0;
                        }

                        areasByCount[area]++;
                    }
                }
            }

            return areasByCount;
        }

        public static int CalculateAsteroidsArea(int row, int col, char[][] matrix, bool[][] visited)
        {
            if (!IsInBounds(matrix, row, col) || visited[row][col] || matrix[row][col] == '0')
            {
                return 0;
            }

            visited[row][col] = true;

            return 1 + CalculateAsteroidsArea(row + 1, col, matrix, visited) +
                CalculateAsteroidsArea(row - 1, col, matrix, visited) +
                CalculateAsteroidsArea(row, col + 1, matrix, visited) +
                CalculateAsteroidsArea(row, col - 1, matrix, visited);
        }

        private static bool IsInBounds(char[][] matrix, int row, int col)
        {
            return row >= 0 && row < matrix.Length && col >= 0 && col < matrix[row].Length;
        }
    }

    public class Cell
    {
        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; private set; }

        public int Col { get; private set; }
    }
}
