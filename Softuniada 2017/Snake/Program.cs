using System;
using System.Linq;

namespace Snake
{
    class Program
    {
        private const char EmptyCell = 'o';
        private const char SnakeStart = 's';
        private const char Apple = 'a';

        private static char[,,] cube;
        private static int size;
        private static int snakeFirstDimension;
        private static int snakeSecondDimension;
        private static int snakeThirdDimension;
        private static string direction;
        private static int collectedPoints = 0;
        private static bool isSnakeDead = false;

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            cube = new char[size, size, size];
            ReadCube();
            direction = Console.ReadLine();

            while (true)
            {
                if(direction == "end" || isSnakeDead)
                {
                    break;
                }

                string[] tokens = Console.ReadLine().Split();
                string nextDirection = tokens[0];
                int steps = int.Parse(tokens[2]);

                Move(steps);

                direction = nextDirection;
            }

            Console.WriteLine($"Points collected: {collectedPoints}");
            if (isSnakeDead)
            {
                Console.WriteLine("The snake dies.");
            }
        }

        private static void Move(int steps)
        {
            for (int i = 0; i < steps; i++)
            {
                if (direction == "up")
                {
                    snakeFirstDimension--;
                }
                else if (direction == "down")
                {
                    snakeFirstDimension++;
                }
                else if (direction == "forward")
                {
                    snakeSecondDimension--;
                }
                else if (direction == "backward")
                {
                    snakeSecondDimension++;
                }
                else if (direction == "left")
                {
                    snakeThirdDimension--;
                }
                else if (direction == "right")
                {
                    snakeThirdDimension++;
                }

                if (!IsInBounds(snakeFirstDimension, snakeSecondDimension, snakeThirdDimension))
                {
                    isSnakeDead = true;
                    break;
                }
                else if (cube[snakeFirstDimension, snakeSecondDimension, snakeThirdDimension] == Apple)
                {
                    collectedPoints++;
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
            for (int secondDimension = 0; secondDimension < cube.GetLength(0); secondDimension++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int firstDimension = 0; firstDimension < cube.GetLength(1); firstDimension++)
                {
                    char[] elements = layers[firstDimension].ToCharArray();

                    for (int thirdDimension = 0; thirdDimension < cube.GetLength(2); thirdDimension++)
                    {
                        if(elements[thirdDimension] == SnakeStart)
                        {
                            snakeFirstDimension = firstDimension;
                            snakeSecondDimension = secondDimension;
                            snakeThirdDimension = thirdDimension;
                        }

                        cube[firstDimension, secondDimension, thirdDimension] = elements[thirdDimension];
                    }
                }
            }
        }
    }
}
