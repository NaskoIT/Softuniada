using System;

namespace EasterEgg
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int width = 5 * n;

            int dotsCount = 2 * n;
            int middleCount = n;
            string dots = new string('.', dotsCount);
            string middle = new string('*', middleCount);
            string topAndBottom = dots + middle + dots;

            Console.WriteLine(topAndBottom);

            for (int i = 0; i < n / 2; i++)
            {
                dotsCount -= 2;
                middleCount += 2;
                dots = new string('.', dotsCount);
                string stars = new string('*', i + 1);
                middle = new string('+', middleCount);

                Console.WriteLine(dots + stars + middle + stars + dots);
            }

            for (int i = 0; i < n / 2; i++)
            {
                dotsCount--;
                middleCount = (width - 4 - 2 * dotsCount);
                dots = new string('.', dotsCount);
                string stars = new string('*', 2);
                middle = new string('=', middleCount);

                Console.WriteLine(dots + stars + middle + stars + dots);
            }

            int tildesCount = (middleCount - 12) / 2;
            string tildes = new string('~', tildesCount);
            Console.WriteLine(dots + "**" + tildes + "HAPPY EASTER" + tildes + "**" + dots);

            for (int i = 0; i < n / 2; i++)
            {
                dots = new string('.', dotsCount);
                string stars = new string('*', 2);
                middle = new string('=', middleCount);

                Console.WriteLine(dots + stars + middle + stars + dots);
                dotsCount++;
                middleCount -= 2;
            }

            for (int i = n / 2; i > 0; i--)
            {
                middleCount = (width - 2 * i - 2 * dotsCount);
                dots = new string('.', dotsCount);
                string stars = new string('*', i);
                middle = new string('+', middleCount);

                Console.WriteLine(dots + stars + middle + stars + dots);
                dotsCount += 2;
            }

            Console.WriteLine(topAndBottom);
        }
    }
}
