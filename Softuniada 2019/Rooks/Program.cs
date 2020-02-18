using System;

namespace Rooks
{
    class Program
    {
        static void Main(string[] args)
        {
            int rows = int.Parse(Console.ReadLine());
            int cols = int.Parse(Console.ReadLine());
            int rooks = int.Parse(Console.ReadLine());

            if(rows  == 1)
            {
                Console.WriteLine(0);
                return;
            }

            long result = (rows * cols) * (rows * cols - 1) / rooks;
            Console.WriteLine(result % 1000001);
        }
    }
}
