using System;
using System.Linq;

namespace ThreeBrothers
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                int[] presents = Console.ReadLine().Split().Select(int.Parse).ToArray();
                Console.WriteLine(CanBeSplitted(presents) ? "Yes" : "No");
            }
        }

        //Dynamic programming solution
        public static bool CanBeSplitted(int[] presents)
        {
            int totalSum = presents.Sum();
            if (totalSum % 3 != 0)
            {
                return false;
            }

            int targetSum = totalSum / 3;
            var reachedSums = new bool[targetSum + 1, targetSum + 1];
            reachedSums[0, 0] = true;
            var prev1 = new int[targetSum + 1, targetSum + 1];
            var prev2 = new int[targetSum + 1, targetSum + 1];

            foreach (var present in presents)
            {
                for (int rowSum = targetSum; rowSum >= 0; rowSum--)
                {
                    for (int colSum = targetSum; colSum >= 0; colSum--)
                    {
                        if (reachedSums[rowSum, colSum])
                        {
                            if (rowSum + present <= targetSum && !reachedSums[rowSum + present, colSum])
                            {
                                reachedSums[rowSum + present, colSum] = true;
                                prev1[rowSum + present, colSum] = present;
                            }
                            if (colSum + present <= targetSum && !reachedSums[rowSum, colSum + present])
                            {
                                reachedSums[rowSum, colSum + present] = true;
                                prev2[rowSum, colSum + present] = present;
                            }
                        }
                    }
                }
            }

            return reachedSums[targetSum, targetSum];
        }
    }
}
