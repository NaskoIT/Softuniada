using System;
using System.Linq;

namespace Elemelons
{
    class Program
    {
        private const char Watermelon = 'W';
        private const char Earthmelon = 'E';
        private const char Firemelon = 'F';
        private const char Airmelon = 'A';
        private static char[,,] cube;
        private static int size;

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            cube = new char[size, size, size];
            ReadCube();

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "Melolemonmelon")
            {
                int[] tokens = command.Split().Select(int.Parse).ToArray();
                int layer = tokens[0];
                int targetRow = tokens[1];
                int targetCol = tokens[2];

                cube[layer, targetRow, targetCol] = '0';
                for (int depth = 0; depth < size; depth++)
                {
                    for (int row = 0; row < size; row++)
                    {
                        for (int col = 0; col < size; col++)
                        {
                            if (layer == depth && row == targetRow && (col == targetCol - 1 || col == targetCol + 1))
                            {
                                continue;
                            }
                            if (layer == depth && col == targetCol && (row == targetRow - 1 || row == targetRow + 1))
                            {
                                continue;
                            }
                            if (col == targetCol && row == targetRow && (depth == layer - 1 || depth == layer + 1))
                            {
                                continue;
                            }

                            cube[depth, row, col] = Morph(cube[depth, row, col]);
                        }
                    }
                }
            }

            for (int row = 0; row < size; row++)
            {
                for (int depth = 0; depth < size; depth++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        Console.Write($"{cube[depth, row, col]} ");
                    }

                    if (depth < size - 1)
                    {
                        Console.Write("| ");
                    }
                }

                Console.WriteLine();
            }
        }

        private static void ReadCube()
        {
            for (int row = 0; row < cube.GetLength(1); row++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int depth = cube.GetLength(0) - 1; depth >= 0; depth--)
                {
                    string[] elements = layers[depth].Split();
                    for (int col = 0; col < cube.GetLength(2); col++)
                    {
                        cube[depth, row, col] = char.Parse(elements[col]);
                    }
                }
            }
        }

        private static char Morph(char initialState)
        {
            switch (initialState)
            {
                case Watermelon:
                    return Earthmelon;
                case Earthmelon:
                    return Firemelon;
                case Firemelon:
                    return Airmelon;
                case Airmelon:
                    return Watermelon;
                default:
                    return initialState;
            }
        }
    }
}
