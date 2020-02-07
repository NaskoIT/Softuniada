using System;

namespace GroupsOfEqualSum
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstNumber = int.Parse(Console.ReadLine());
            int secondNumber = int.Parse(Console.ReadLine());
            int thirdNumber = int.Parse(Console.ReadLine());
            int fourthNumber = int.Parse(Console.ReadLine());

            double halfSum = (firstNumber + secondNumber + thirdNumber + fourthNumber) / 2.0;

            if(firstNumber == halfSum ||
               secondNumber == halfSum ||
               thirdNumber == halfSum ||
               fourthNumber == halfSum ||
               firstNumber + secondNumber == halfSum ||
               firstNumber + thirdNumber == halfSum ||
               firstNumber + fourthNumber == halfSum)
            {
                Console.WriteLine("Yes");
                Console.WriteLine(halfSum);
            }
            else
            {
                Console.WriteLine("No");
            }
        }
    }
}
