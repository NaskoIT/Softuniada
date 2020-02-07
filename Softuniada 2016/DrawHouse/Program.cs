using System;

namespace DrawHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int width = 2 * n - 1;

            Console.WriteLine(string.Format("{0}*{0}", new string('.', n - 1)));

            for (int i = 0; i < n - 2; i++)
            {
                Console.WriteLine(string.Format("{0}*{1}*{0}", new string('.', n - 2 - i), new string(' ', 2 * i + 1)));
            }

            for (int i = 0; i < n; i++)
            {
                Console.Write('*');
                if(i < n - 1)
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
            string houseBase = $"+{new string('-', width - 2)}+";
            Console.WriteLine(houseBase);

            for (int i = 0; i < n - 2; i++)
            {
                Console.WriteLine($"|{new string(' ', width - 2)}|");
            }

            Console.WriteLine(houseBase);
        }
    }
}
