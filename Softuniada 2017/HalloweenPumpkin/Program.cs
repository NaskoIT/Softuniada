using System;

namespace HalloweenPumpkin
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            int dotsCount = n - 1;
            string dots = new string('.', dotsCount);
            Console.WriteLine($"{dots}_/_{dots}");

            dots = new string('.', --dotsCount);
            Console.WriteLine($"/{dots}^,^{dots}\\");

            for (int i = 0; i < n - 3; i++)
            {
                Console.WriteLine($"|{new string('.', 2 * n - 1)}|");
            }

            Console.WriteLine($"\\{dots}\\_/{dots}/");
        }
    }
}
