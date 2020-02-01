using System;
using System.Linq;

namespace WrongResults
{
    class Program
    {
        private static int[,,] cube;
        private static int[,,] correctCube;
        private static int size;
        private static Coordinates[] neighbours = new Coordinates[]
        {
            new Coordinates(1, 0, 0), // back
            new Coordinates(-1, 0, 0), // front
            new Coordinates(0, -1, 0), // up
            new Coordinates(0, 1, 0), // down
            new Coordinates(0, 0, 1), // right
            new Coordinates(0, 0, -1) // left
        };

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            cube = new int[size, size, size];
            correctCube = new int[size, size, size];
            ReadCube();
            int[] coordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var incorrectValueCoordinates = new Coordinates(coordinates[0], coordinates[1], coordinates[2]);

            int incorrectValue = cube[incorrectValueCoordinates.X, incorrectValueCoordinates.Y, incorrectValueCoordinates.Z];

            int changedValuesCount = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        if (cube[x, y, z] == incorrectValue)
                        {
                            int sum = 0;
                            foreach (var neighbour in neighbours)
                            {
                                int neighbourX = x + neighbour.X;
                                int neighbourY = y + neighbour.Y;
                                int neighbourZ = z + neighbour.Z;
                                if (IsInBounds(neighbourX, neighbourY, neighbourZ))
                                {
                                    int neighbourValue = cube[neighbourX, neighbourY, neighbourZ];
                                    if (neighbourValue != incorrectValue)
                                    {
                                        sum += neighbourValue;
                                    }
                                }
                            }

                            changedValuesCount++;
                            correctCube[x, y, z] = sum;
                        }
                    }
                }
            }

            Console.WriteLine($"Wrong values found and replaced: {changedValuesCount}");
            PrinCube(correctCube);
        }

        private static void PrinCube(int[,,] cube)
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        Console.Write(cube[x, y, z] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static bool IsInBounds(int neighbourX, int neighbourY, int neighbourZ)
        {
            return IsInBounds(neighbourX) && IsInBounds(neighbourY) && IsInBounds(neighbourZ);
        }

        private static bool IsInBounds(int coordinate)
        {
            return coordinate >= 0 && coordinate < size;
        }

        private static void ReadCube()
        {
            for (int secondDimension = 0; secondDimension < cube.GetLength(0); secondDimension++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int firstDimension = 0; firstDimension < cube.GetLength(1); firstDimension++)
                {
                    int[] elements = layers[firstDimension].Trim().Split().Select(int.Parse).ToArray();

                    for (int thirdDimension = 0; thirdDimension < cube.GetLength(2); thirdDimension++)
                    {
                        cube[firstDimension, secondDimension, thirdDimension] = elements[thirdDimension];
                        correctCube[firstDimension, secondDimension, thirdDimension] = elements[thirdDimension];
                    }
                }
            }
        }
    }

    public class Coordinates
    {
        public Coordinates(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Z { get; set; }
    }
}
