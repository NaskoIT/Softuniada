using System;
using System.Collections.Generic;
using System.Linq;

namespace StairsInTheCube
{
    class Program
    {
        private static char[,,] cube;
        private static readonly Dictionary<char, int> letterByStairsCount = new Dictionary<char, int>();

        static void Main(string[] args)
        {
            int cubeSize = int.Parse(Console.ReadLine());
            cube = new char[cubeSize, cubeSize, cubeSize];
            ReadCube();

            FindAllStairs();

            PrintResult();
        }

        private static void PrintResult()
        {
            Console.WriteLine(letterByStairsCount.Values.Sum());

            foreach (var letterByStair in letterByStairsCount.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{letterByStair.Key} -> {letterByStair.Value}");
            }
        }

        private static void FindAllStairs()
        {
            for (int depth = 0; depth < cube.GetLength(0) - 2; depth++)
            {
                for (int row = 1; row < cube.GetLength(1) - 1; row++)
                {
                    for (int col = 1; col < cube.GetLength(2) - 1; col++)
                    {
                        char currentLettter = cube[depth, row, col];

                        if (cube[depth + 1, row, col] == currentLettter &&
                           cube[depth + 1, row + 1, col] == currentLettter &&
                           cube[depth + 1, row - 1, col] == currentLettter &&
                           cube[depth + 1, row, col + 1] == currentLettter &&
                           cube[depth + 1, row, col - 1] == currentLettter &&
                           cube[depth + 2, row, col] == currentLettter)
                        {
                            if (!letterByStairsCount.ContainsKey(currentLettter))
                            {
                                letterByStairsCount.Add(currentLettter, 0);
                            }

                            letterByStairsCount[currentLettter]++;
                        }
                    }
                }
            }
        }

        private static void ReadCube()
        {
            for (int row = 0; row < cube.GetLength(1); row++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int depth = 0; depth < cube.GetLength(0); depth++)
                {
                    char[] letters = layers[depth].Split().Select(char.Parse).ToArray();

                    for (int col = 0; col < cube.GetLength(2); col++)
                    {
                        cube[depth, row, col] = letters[col];
                    }
                }
            }
        }
    }
}
