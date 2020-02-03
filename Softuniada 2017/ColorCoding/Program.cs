using System;

namespace ColorCoding
{
    class Program
    {
        private const int TransformableColors = 2;
        private const int PartiallyTransformableColors = 1;

        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                var firstSequence = Console.ReadLine().Split();
                var secondSequence = Console.ReadLine().Split();

                if (firstSequence.Length < secondSequence.Length)
                {
                    Console.WriteLine("false");
                    continue;
                }

                bool isTransformable = IsTransformable(firstSequence, secondSequence);
                Console.WriteLine(isTransformable.ToString().ToLower());
            }
        }

        private static bool IsTransformable(string[] firstSequence, string[] secondSequence)
        {
            int[,] transformations = new int[secondSequence.Length + 1, firstSequence.Length + 1];
            for (int row = 0; row < secondSequence.Length; row++)
            {
                transformations[row, 0] = 1;
            }

            for (int col = 0; col < firstSequence.Length; col++)
            {
                transformations[0, col] = 1;
            }

            for (int row = 1; row <= secondSequence.Length; row++)
            {
                for (int col = 1; col <= firstSequence.Length; col++)
                {
                    string startColor = firstSequence[col - 1];
                    string endColor = secondSequence[row - 1];

                    if (startColor == endColor)
                    {
                        if(transformations[row - 1, col - 1] == TransformableColors)
                        {
                            transformations[row, col] = 0;
                        }
                        else
                        {
                            transformations[row, col] = transformations[row - 1, col - 1];
                        }

                        continue;
                    }

                    if (IsPartialColor(startColor))
                    {
                        string partalColor = ExtractPartialColor(startColor);
                        if(partalColor == endColor)
                        {
                            transformations[row, col] = Math.Min(transformations[row - 1, col - 1], PartiallyTransformableColors);
                        }
                        else
                        {
                            transformations[row, col] = transformations[row, col - 1];
                        }
                    }
                    else
                    {
                        transformations[row, col] = 0;
                    }
                }
            }

            return transformations[secondSequence.Length, firstSequence.Length] > 0;
        }

        private static bool IsPartialColor(string color)
        {
            return color[0] == '(' && color[color.Length - 1] == ')';
        }

        private static string ExtractPartialColor(string partialColor)
        {
            return partialColor.Substring(1, partialColor.Length - 2);
        }
    }
}
