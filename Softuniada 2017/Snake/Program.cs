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
        private static int snakeDepth;
        private static int snakeRow;
        private static int snakeCol;
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
                    snakeDepth--;
                }
                else if (direction == "down")
                {
                    snakeDepth++;
                }
                else if (direction == "forward")
                {
                    snakeRow--;
                }
                else if (direction == "backward")
                {
                    snakeRow++;
                }
                else if (direction == "left")
                {
                    snakeCol--;
                }
                else if (direction == "right")
                {
                    snakeCol++;
                }

                if (!IsInBounds(snakeDepth, snakeRow, snakeCol))
                {
                    isSnakeDead = true;
                    break;
                }
                else if (cube[snakeDepth, snakeRow, snakeCol] == Apple)
                {
                    cube[snakeDepth, snakeRow, snakeCol] = EmptyCell;
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
            for (int row = 0; row < cube.GetLength(1); row++)
            {
                string[] layers = Console.ReadLine().Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);

                for (int depth = 0; depth < cube.GetLength(0); depth++)
                {
                    for (int col = 0; col < cube.GetLength(2); col++)
                    {
                        if(layers[depth][col] == SnakeStart)
                        {
                            snakeDepth = depth;
                            snakeRow = row;
                            snakeCol = col;
                        }

                        cube[depth, row, col] = layers[depth][col];
                    }
                }
            }
        }
    }
}
