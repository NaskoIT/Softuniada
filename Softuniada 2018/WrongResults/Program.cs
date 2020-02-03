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
            new Coordinates(1, 0, 0), // right
            new Coordinates(-1, 0, 0), // left
            new Coordinates(0, -1, 0), // backward 
            new Coordinates(0, 1, 0), // forward
            new Coordinates(0, 0, 1), // down
            new Coordinates(0, 0, -1) // up
        };

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            cube = new int[size, size, size];
            correctCube = new int[size, size, size];
            ReadCube();
            int[] coordinates = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var incorrectValueCoordinates = new Coordinates(coordinates[2], coordinates[1], coordinates[0]);

            int incorrectValue = cube[incorrectValueCoordinates.Z, incorrectValueCoordinates.Y, incorrectValueCoordinates.X];

            int changedValuesCount = 0;

            for (int firstDimension = 0; firstDimension < size; firstDimension++)
            {
                for (int secondDimension = 0; secondDimension < size; secondDimension++)
                {
                    for (int thirdDimension = 0; thirdDimension < size; thirdDimension++)
                    {
                        //firstDimension == Z
                        //secondDimension == y
                        //thirdDimension == x
                        if (cube[firstDimension, secondDimension, thirdDimension] == incorrectValue)
                        {
                            int sum = 0;
                            foreach (var neighbour in neighbours)
                            {
                                int neighbourZ = firstDimension + neighbour.Z;
                                int neighbourY = secondDimension + neighbour.Y;
                                int neighbourX = thirdDimension + neighbour.X;
                                if (IsInBounds(neighbourZ, neighbourY, neighbourX))
                                {
                                    int neighbourValue = cube[neighbourZ, neighbourY, neighbourX];
                                    if (neighbourValue != incorrectValue)
                                    {
                                        sum += neighbourValue;
                                    }
                                }
                            }

                            changedValuesCount++;
                            correctCube[firstDimension, secondDimension, thirdDimension] = sum;
                        }
                    }
                }
            }

            Console.WriteLine($"Wrong values found and replaced: {changedValuesCount}");
            PrintCube(correctCube);
        }

        private static void PrintCube(int[,,] cube)
        {
            for (int firstDimension = 0; firstDimension < size; firstDimension++)
            {
                for (int secondDimension = 0; secondDimension < size; secondDimension++)
                {
                    for (int thirdDemension = 0; thirdDemension < size; thirdDemension++)
                    {
                        Console.Write(cube[firstDimension, secondDimension, thirdDemension] + " ");
                    }

                    Console.WriteLine();
                }
            }
        }

        private static bool IsInBounds(int x, int y, int z)
        {
            return IsInBounds(x) && IsInBounds(y) && IsInBounds(z);
        }

        private static bool IsInBounds(int coordinate)
        {
            return coordinate >= 0 && coordinate < size;
        }

        private static void ReadCube()
        {
            for (int secondDimension = 0; secondDimension < cube.GetLength(1); secondDimension++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int firstDimension = 0; firstDimension < cube.GetLength(0); firstDimension++)
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
