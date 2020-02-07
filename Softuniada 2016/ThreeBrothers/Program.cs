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
                Console.WriteLine(CanBeSplittedToThreeEqualPartsUsnigDynamicProgramming(presents) ? "Yes" : "No");
            }
        }

        /// <summary>
        ///  Dynamic programming solution
        /// </summary>
        /// <param name="presents">Array of integers representing the prices of the presents</param>
        /// <returns>True if the presents can be splitted to three equal parts and false otherwise</returns>
        public static bool CanBeSplittedToThreeEqualPartsUsnigDynamicProgramming(int[] presents)
        {
            int totalSum = presents.Sum();
            if (totalSum % 3 != 0)
            {
                return false;
            }

            int targetSum = totalSum / 3;
            var reachedSums = new bool[targetSum + 1, targetSum + 1];
            reachedSums[0, 0] = true;

            foreach (var present in presents)
            {
                for (int rowSum = targetSum; rowSum >= 0; rowSum--)
                {
                    for (int colSum = targetSum; colSum >= 0; colSum--)
                    {
                        if (reachedSums[rowSum, colSum])
                        {
                            if (rowSum + present <= targetSum)
                            {
                                reachedSums[rowSum + present, colSum] = true;
                            }
                            if (colSum + present <= targetSum)
                            {
                                reachedSums[rowSum, colSum + present] = true;
                            }
                        }
                    }
                }
            }

            return reachedSums[targetSum, targetSum];
        }

        /// <summary>
        ///  Recursion and backtracking
        /// </summary>
        /// <param name="presents">Array of integers representing the prices of the presents</param>
        /// <returns>True if the presents can be splitted to three equal parts and false otherwise</returns>
        public static bool CanBeSplittedToThreeEqualPartsUsnigBackTracking(int[] presents)
        {
            int totalSum = presents.Sum();
            if (totalSum % 3 != 0)
            {
                // The presents cannot be divided into 3 equal integer numbers
                return false;
            }

            int targetSum = totalSum / 3;

            bool found = false;
            FindSum(0, 0, 0, ref found, targetSum, presents);
            return found;
        }

        private static void FindSum(int firstSum, int secondSum, int index, ref bool found, int targetSum, int[] presents)
        {
            if (found || firstSum > targetSum || secondSum > targetSum || index == presents.Length)
            {
                return;
            }

            if (firstSum == targetSum && secondSum == targetSum)
            {
                found = true;
                return;
            }

            FindSum(firstSum + presents[index], secondSum, index + 1, ref found, targetSum, presents);

            FindSum(firstSum, secondSum + presents[index], index + 1, ref found, targetSum, presents);

            FindSum(firstSum, secondSum, index + 1, ref found, targetSum, presents);
        }
    }
}
