using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopKeeper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var stocks = new HashSet<int>(Console.ReadLine().Split().Select(int.Parse));
            var notCoveredStocks = new HashSet<int>(stocks);
            int[] ordersInput = Console.ReadLine().Split().Select(int.Parse).ToArray();

            SortedSet<Order> ordersByOrderTime = new SortedSet<Order>();
            SortedSet<Order> stocksByOrderTime = new SortedSet<Order>();
            Dictionary<int, Order> orderItemTypes = new Dictionary<int, Order>();

            for (int i = 0; i < ordersInput.Length; i++)
            {
                int currentItemType = ordersInput[i];

                if (!orderItemTypes.ContainsKey(currentItemType))
                {
                    orderItemTypes.Add(currentItemType, new Order(currentItemType));
                }

                orderItemTypes[currentItemType].OrderPositions.Add(i);
            }

            foreach (var orderItemType in orderItemTypes)
            {
                ordersByOrderTime.Add(orderItemType.Value);

                if (stocks.Contains(orderItemType.Key))
                {
                    stocksByOrderTime.Add(orderItemType.Value);
                    notCoveredStocks.Remove(orderItemType.Key);
                }
            }

            foreach (var itemType in notCoveredStocks)
            {
                stocksByOrderTime.Add(new Order(itemType));
            }

            int changes = 0;
            bool solution = true;

            for (int i = 0; i < ordersInput.Length - 1; i++)
            {
                int itemType = ordersInput[i];

                if (!stocks.Contains(itemType))
                {
                    solution = false;
                    break;
                }

                Order currentOrder = ordersByOrderTime.Min;
                ordersByOrderTime.Remove(currentOrder);
                stocksByOrderTime.Remove(currentOrder);
                currentOrder.OrderPositions.RemoveAt(0);
                ordersByOrderTime.Add(currentOrder);
                stocksByOrderTime.Add(currentOrder);

                Order nextOrder = ordersByOrderTime.Min;

                if (!stocks.Contains(nextOrder.ItemType))
                {
                    Order maxOrder = stocksByOrderTime.Max;
                    stocks.Remove(maxOrder.ItemType);
                    stocksByOrderTime.Remove(maxOrder);
                    stocksByOrderTime.Add(nextOrder);
                    stocks.Add(nextOrder.ItemType);
                    changes++;
                }
            }

            if (!stocks.Contains(ordersInput.Last()))
            {
                solution = false;
            }

            Console.WriteLine(solution ? changes.ToString() : "impossible");
        }
    }

    public class Order : IComparable<Order>
    {
        public Order(int itemType)
        {
            ItemType = itemType;
            OrderPositions = new List<int>();
        }

        public int ItemType { get; set; }

        public List<int> OrderPositions { get; set; }

        public int CompareTo(Order other)
        {
            if (ItemType == other.ItemType)
            {
                return 0;
            }

            if (!OrderPositions.Any())
            {
                return 1;
            }
            else if (!other.OrderPositions.Any())
            {
                return -1;
            }

            return OrderPositions[0].CompareTo(other.OrderPositions[0]);
        }
    }
}
