using System;
using System.Linq;

namespace ColorCoding
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Color[] firstSequence = Console.ReadLine().Split().Select(color => new Color(color)).ToArray();
                Color[] targetSequence = Console.ReadLine().Split().Select(color => new Color(color)).ToArray();

                if (firstSequence.Length < targetSequence.Length)
                {
                    Console.WriteLine("false");
                    continue;
                }

                bool isTransformable = IsTransformable(firstSequence, targetSequence);
                Console.WriteLine(isTransformable.ToString().ToLower());
            }
        }

        private static bool IsTransformable(Color[] firstSequence, Color[] targetSequence)
        {
            bool[,] matrix = new bool[firstSequence.Length + 1, targetSequence.Length + 1];
            matrix[0, 0] = true;

            for (int firstIndex = 0; firstIndex < firstSequence.Length; firstIndex++)
            {
                Color firstColor = firstSequence[firstIndex];

                for (int targetIndex = 0; targetIndex <= targetSequence.Length; targetIndex++)
                {
                    if (!matrix[firstIndex, targetIndex])
                    {
                        continue;
                    }
                    if (!firstColor.IsFull)
                    {
                        matrix[firstIndex + 1, targetIndex] = true;
                    }
                    if (targetIndex < targetSequence.Length && firstColor.ColorText == targetSequence[targetIndex].ColorText)
                    {
                        matrix[firstIndex + 1, targetIndex + 1] = true;
                    }
                }
            }

            return matrix[firstSequence.Length, targetSequence.Length];
        }
    }

    public class Color
    {
        public Color(string color)
        {
            if (!IsPartialColor(color))
            {
                IsFull = true;
            }

            if (IsFull)
            {
                ColorText = color;
            }
            else
            {
                ColorText = ExtractPartialColor(color);
            }
        }

        public bool IsFull { get; set; }

        public string ColorText { get; set; }

        private bool IsPartialColor(string color)
        {
            return color[0] == '(' && color[color.Length - 1] == ')';
        }

        private string ExtractPartialColor(string partialColor)
        {
            return partialColor.Substring(1, partialColor.Length - 2);
        }
    }
}
