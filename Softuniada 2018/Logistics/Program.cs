using System;
using System.Collections.Generic;
using System.Linq;

namespace Logistics
{
    class Program
    {
        private const int VanRepairSum = 50;
        private static readonly Dictionary<int, List<Package>> packages = new Dictionary<int, List<Package>>();
        private static HashSet<int> accidents = new HashSet<int>();

        static void Main(string[] args)
        {
            int amountOfPackages = int.Parse(Console.ReadLine());
            int maxDeadline = 0;
            for (int i = 1; i <= amountOfPackages; i++)
            {
                var packageParts = Console.ReadLine().Split().Select(int.Parse).ToArray();
                var package = new Package
                {
                    Id = i,
                    Price = packageParts[0],
                    Deadline = packageParts[1]
                };

                if (maxDeadline < package.Deadline)
                {
                    maxDeadline = package.Deadline;
                }
                if (!packages.ContainsKey(package.Deadline))
                {
                    packages[package.Deadline] = new List<Package>();
                }

                packages[package.Deadline].Add(package);
            }

            string crachesInput = Console.ReadLine();
            if (crachesInput != "none")
            {
                accidents = new HashSet<int>(crachesInput.Split().Select(int.Parse));
            }

            var sortedPackages = new SortedSet<Package>();
            var bestPackages = new List<int>();
            long total = 0;

            for (int deadline = maxDeadline; deadline >= 1; deadline--)
            {
                if (packages.ContainsKey(deadline))
                {
                    foreach (var package in packages[deadline])
                    {
                        sortedPackages.Add(package);
                    }
                }

                Package bestPackage = null;
                if (accidents.Contains(deadline))
                {
                    bestPackage = sortedPackages.Min;
                    total -= VanRepairSum;
                    total -= bestPackage.Price;
                }
                else
                {
                    bestPackage = sortedPackages.Max;
                    total += bestPackage.Price;
                }

                sortedPackages.Remove(bestPackage);
                bestPackages.Add(bestPackage.Id);
            }

            Console.WriteLine(total);
            bestPackages.Reverse();
            Console.WriteLine(string.Join(" ", bestPackages));
        }
    }

    public class Package : IComparable<Package>
    {
        public int Id { get; set; }

        public int Price { get; set; }

        public int Deadline { get; set; }

        public int CompareTo(Package other)
        {
            int compare = Price.CompareTo(other.Price);
            if (compare == 0)
            {
                compare = Deadline.CompareTo(other.Deadline);
                if (compare == 0)
                {
                    compare = Id.CompareTo(other.Id);
                }
            }

            return compare;
        }
    }
}
