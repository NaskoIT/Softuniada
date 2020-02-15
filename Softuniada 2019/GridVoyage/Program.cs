using System;

namespace GridVoyage
{
    class Program
    {
        private static int[,] matrix;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            matrix = new int[n, n];

            var initialPosition = Console.ReadLine().Split();
            int startRow = int.Parse(initialPosition[0]);
            int startCol = int.Parse(initialPosition[1]);

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "eastern odyssey")
            {
                var tokens = command.Split();
                int row = int.Parse(tokens[0]);
                int col = int.Parse(tokens[1]);
                string direction = tokens[2];
                int stamina = int.Parse(tokens[3]);
            }
        }
    }
}
