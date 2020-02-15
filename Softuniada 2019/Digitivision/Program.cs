using System;

namespace Digitivision
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());

            if (IsDivisible(a, b, c) ||
                IsDivisible(a, c, b) ||
                IsDivisible(b, a, c) ||
                IsDivisible(b, c, a) ||
                IsDivisible(c, a, b) ||
                IsDivisible(c, b, a))
            {
                Console.WriteLine("Digitivision successful!");
            }
            else
            {
                Console.WriteLine("No digitivision possible.");
            }
        }

        private static bool IsDivisible(int a, int b, int c)
        {
            int number = a * 100 + b * 10 + c;
            int sum = a + b + c;
            if(sum == 0)
            {
                return false;
            }

            return number % sum == 0;
        }
    }
}
