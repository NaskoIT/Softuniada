using System;
using System.Linq;

namespace SumTo13
{
    class Program
    {
        private const int TargetSum = 13;
        private static bool TragetSumIsReachable;

        static void Main(string[] args)
        {
            int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Sum(numbers, 0, 0);
            Console.WriteLine(TragetSumIsReachable ? "Yes" : "No");
        }

        public static void Sum(int[] numbers, int currentSum, int index)
        {
            if(index == numbers.Length)
            {
                if(TargetSum == currentSum)
                {
                    TragetSumIsReachable = true;
                }

                return;
            }

            Sum(numbers, currentSum - numbers[index], index + 1);
            Sum(numbers, currentSum + numbers[index], index + 1);

        }
    }
}
