using System;

namespace GridVoyage
{
    class Program
    {
        private static int[,] matrix;
        private static int targetRow;
        private static int targetCol;
        private const string Left = "left";
        private const string Right = "right";
        private const string Down = "down";
        private const string Up = "up";
        private static int size;

        static void Main(string[] args)
        {
            size = int.Parse(Console.ReadLine());
            matrix = new int[size, size];

            var initialPosition = Console.ReadLine().Split();
            int row = int.Parse(initialPosition[0]);
            int col = int.Parse(initialPosition[1]);

            string command = string.Empty;
            while ((command = Console.ReadLine()) != "eastern odyssey")
            {
                var tokens = command.Split();
                targetRow = int.Parse(tokens[0]);
                targetCol = int.Parse(tokens[1]);
                string direction = tokens[2];
                int initialStamina = int.Parse(tokens[3]);
                int rests = -1;
                bool isVoyagePossible = Math.Abs(targetRow - row) % initialStamina == 0 && Math.Abs(targetCol - col) % initialStamina == 0;
                if (!isVoyagePossible)
                {
                    Console.WriteLine("Voyage impossible");
                    continue;
                }

                while (true)
                {
                    int currentStamina = initialStamina;
                    rests++;
                    if (!IsPossibleTurn(row, col, currentStamina, direction))
                    {
                        isVoyagePossible = false;
                        break;
                    }

                    while (currentStamina > 0)
                    {
                        if (direction == "left")
                        {
                            col--;
                        }
                        else if (direction == "right")
                        {
                            col++;
                        }
                        else if (direction == "down")
                        {
                            row++;
                        }
                        else if (direction == "up")
                        {
                            row--;
                        }

                        matrix[row, col]++;
                        currentStamina--;
                    }

                    if (row == targetRow && col == targetCol)
                    {
                        break;
                    }

                    direction = GetNextDirection(row, col, direction, targetRow, targetCol, initialStamina);

                    if (direction == null)
                    {
                        isVoyagePossible = false;
                        break;
                    }
                }

                if (!isVoyagePossible)
                {
                    Console.WriteLine("Voyage impossible");
                }
                else
                {
                    Console.WriteLine(rests);
                }
            }

            PrintMatrix();
        }

        private static bool IsPossibleTurn(int row, int col, int currentStamina, string direction)
        {
            bool isPossible = false;

            if (direction == Right && col + currentStamina < size)
            {
                isPossible = true;
            }
            else if (direction == Left && col - currentStamina >= 0)
            {
                isPossible = true;
            }
            else if (direction == Down && row + currentStamina < size)
            {
                isPossible = true;
            }
            else if (direction == Up && row - currentStamina >= 0)
            {
                isPossible = true;
            }

            return isPossible;
        }

        private static void PrintMatrix()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    Console.Write($"{matrix[row, col]} ");
                }

                Console.WriteLine();
            }
        }

        private static string GetNextDirection(int row, int col, string direction, int targetRow, int targetCol, int stamina)
        {
            string nextDirection = null;

            if (direction == Left)
            {
                if (targetRow >= row && row + stamina < size)
                {
                    nextDirection = Down;
                }
                else if (row - stamina >= 0)
                {
                    nextDirection = Up;
                }
            }
            else if (direction == Right)
            {
                if (targetRow <= row && row - stamina >= 0)
                {
                    nextDirection = Up;
                }
                else if (row + stamina < size)
                {
                    nextDirection = Down;
                }
            }
            else if (direction == Up)
            {
                if (targetCol <= col && col - stamina >= 0)
                {
                    nextDirection = Left;
                }
                else if (col + stamina < size)
                {
                    nextDirection = Right;
                }
            }
            else if (direction == Down)
            {
                if (targetCol >= col && col + stamina < size)
                {
                    nextDirection = Right;
                }
                else if (col - stamina >= 0)
                {
                    nextDirection = Left;
                }
            }

            return nextDirection;
        }
    }
}
