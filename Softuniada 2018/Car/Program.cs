using System;
using System.Linq;

namespace Car
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            int[] speedChanges = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int initialSpeed = int.Parse(Console.ReadLine());
            int maxSpeed = int.Parse(Console.ReadLine());
            int minSpeed = 0;

            bool[,] possibleSpeeds = new bool[speedChanges.Length + 1, maxSpeed + 1];
            possibleSpeeds[0, initialSpeed] = true;

            for (int row = 1; row < possibleSpeeds.GetLength(0); row++)
            {
                for (int currentSpeed = 0; currentSpeed < possibleSpeeds.GetLength(1); currentSpeed++)
                {
                    int currentSpeedChange = speedChanges[row - 1];

                    if (possibleSpeeds[row - 1, currentSpeed])
                    {
                        if (currentSpeed - currentSpeedChange >= minSpeed)
                        {
                            possibleSpeeds[row, currentSpeed - currentSpeedChange] = true;
                        }

                        if (currentSpeed + currentSpeedChange <= maxSpeed)
                        {
                            possibleSpeeds[row, currentSpeed + currentSpeedChange] = true;
                        }
                    }
                }
            }

            int maxPossibleSpeed = -1;

            for (int possibleSpeed = possibleSpeeds.GetLength(1) - 1; possibleSpeed >= 0; possibleSpeed--)
            {
                if (possibleSpeeds[possibleSpeeds.GetLength(0) - 1, possibleSpeed])
                {
                    maxPossibleSpeed = possibleSpeed;
                    break;
                }
            }

            Console.WriteLine(maxPossibleSpeed);
        }
    }
}
