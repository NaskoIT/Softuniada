using System;
using System.Collections.Generic;
using System.Linq;

namespace Nexus
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> firstSequence = Console.ReadLine().Split().Select(int.Parse).ToList();
            List<int> secondSequence = Console.ReadLine().Split().Select(int.Parse).ToList();

            string comamnd = string.Empty;
            while ((comamnd = Console.ReadLine()) != "nexus")
            {
                int[] tokens = comamnd.Split(new[] { ':', '|'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                int firstSequenceStartIndex = tokens[0];
                int firstSequenceEndIndex = tokens[2];
                int secondSequenceStartIndex = tokens[3];
                int secondSequenceEndIndex = tokens[1];

                if(firstSequenceEndIndex > secondSequenceStartIndex && firstSequenceStartIndex < secondSequenceEndIndex)
                {
                    int value = firstSequence[firstSequenceStartIndex] + firstSequence[firstSequenceEndIndex] +
                        secondSequence[secondSequenceStartIndex] + secondSequence[secondSequenceEndIndex];
                    UpdateList(firstSequence, firstSequenceStartIndex, firstSequenceEndIndex, value);
                    UpdateList(secondSequence, secondSequenceStartIndex, secondSequenceEndIndex, value);
                }
            }

            Console.WriteLine(string.Join(", ", firstSequence));
            Console.WriteLine(string.Join(", ", secondSequence));
        }

        private static void UpdateList(List<int> sequence, int startIndex, int endIndex, int value)
        {
            sequence.RemoveRange(startIndex, endIndex - startIndex + 1);

            for (int i = 0; i < sequence.Count; i++)
            {
                sequence[i] += value;
            }
        }
    }
}
