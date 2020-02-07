using System;
using System.Linq;

namespace SumTime
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan startTime = PraseTime();
            TimeSpan endTime = PraseTime();

            TimeSpan sumTime = startTime + endTime;
            Console.WriteLine($"{(sumTime.Days == 0 ? "" : $"{sumTime.Days}::")}{sumTime.Hours}:{sumTime.Minutes:D2}");
        }

        private static TimeSpan PraseTime()
        {
            int[] tokens = Console.ReadLine().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int hours = tokens[tokens.Length - 2];
            int minutes = tokens[tokens.Length - 1];
            int days = 0;

            if (tokens.Length > 2)
            {
                days = tokens[tokens.Length - 3];
            }

            return new TimeSpan(days, hours, minutes, 0);
        }
    }
}
