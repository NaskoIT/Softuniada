using System;

namespace Crocs
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int width = n * 5;
            int height = n * 4 - 2;

            PrintTopAndBottom(n);
            string start = new string('#', n);
            PrintBorder(n, start);

            string evenLines = $"{start} {Repeat("# ", (3 * n) / 2)}{start}";
            string oddLines = $"{start}  {Repeat("# ", (3 * n) / 2 - 1)} {start}";
            for (int i = 0; i < n * 2 - 1; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(evenLines);
                }
                else
                {
                    Console.WriteLine(oddLines);
                }
            }

            PrintBorder(n, start);

            oddLines = evenLines;
            evenLines = new string('#', width);

            for (int i = 0; i < n + 2; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(evenLines);
                }
                else
                {
                    Console.WriteLine(oddLines);
                }
            }

            PrintTopAndBottom(n);
        }

        private static void PrintBorder(int n, string start)
        {
            Console.WriteLine($"{start}{new string(' ', 3 * n)}{start}");
        }

        private static string Repeat(string element, int n)
        {
            string result = string.Empty;
            for (int i = 0; i < n; i++)
            {
                result += element;
            }

            return result;
        }

        private static void PrintTopAndBottom(int n)
        {
            string topAndBottom = $"{new string(' ', n)}{new string('#', 3 * n)}{new string(' ', n)}";
            
            for (int i = 0; i < n / 2; i++)
            {
                Console.WriteLine(topAndBottom);
            }
        }
    }
}
