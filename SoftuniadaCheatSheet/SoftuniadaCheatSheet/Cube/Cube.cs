using System;
using System.Linq;

namespace SoftuniadaCheatSheet.Cube
{
    public static class Cube
    {
        public static int[,,] cube;
        public static int size;

        public static void InitializeCube(int cubeSize)
        {
            size = cubeSize;
            cube = new int[size, size, size];
        }

        public static void ReadCube()
        {
            for (int row = 0; row < cube.GetLength(1); row++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int depth = 0; depth < cube.GetLength(0); depth++)
                {
                    int[] line = layers[depth].Split().Select(int.Parse).ToArray();

                    for (int col = 0; col < cube.GetLength(2); col++)
                    {
                        cube[depth, row, col] = line[col];
                    }
                }
            }
        }

        public static bool IsInBounds(int coordinate)
        {
            return coordinate >= 0 && coordinate < size;
        }

        public static bool IsInBounds(int x, int y, int z)
        {
            return IsInBounds(x) && IsInBounds(y) && IsInBounds(z);
        }

        public static void Print()
        {
            for (int depth = 0; depth < size; depth++)
            {
                for (int row = 0; row < size; row++)
                {
                    for (int col = 0; col < size; col++)
                    {
                        Console.Write($"{cube[depth, row, col]} ");
                    }

                    Console.WriteLine();
                }
            }
        }

        public static void Move(int steps, string direction, ref int depth, ref int row, ref int col)
        {
            for (int i = 0; i < steps; i++)
            {
                if (direction == "up")
                {
                    depth--;
                }
                else if (direction == "down")
                {
                    depth++;
                }
                else if (direction == "forward")
                {
                    row--;
                }
                else if (direction == "backward")
                {
                    row++;
                }
                else if (direction == "left")
                {
                    col--;
                }
                else if (direction == "right")
                {
                    col++;
                }

                if (!IsInBounds(depth, row, col))
                {
                    throw new ArgumentException("Some of the coordinates is outside cube bounds");
                }
            }
        }

        public static void CheckNeighbours()
        {
            for (int depth = 0; depth < cube.GetLength(0) - 2; depth++)
            {
                for (int row = 1; row < cube.GetLength(1) - 1; row++)
                {
                    for (int col = 1; col < cube.GetLength(2) - 1; col++)
                    {
                        int currentNumber = cube[depth, row, col];

                        if (cube[depth + 1, row, col] == currentNumber &&
                           cube[depth + 1, row + 1, col] == currentNumber &&
                           cube[depth + 1, row - 1, col] == currentNumber &&
                           cube[depth + 1, row, col + 1] == currentNumber &&
                           cube[depth + 1, row, col - 1] == currentNumber &&
                           cube[depth + 2, row, col] == currentNumber)
                        {
                            //TODO: Write your logic here
                        }
                    }
                }
            }
        }
    }
}
