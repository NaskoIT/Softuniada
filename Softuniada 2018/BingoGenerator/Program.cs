using System;
using System.Collections.Generic;

namespace BingoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int thousandDigit = n / 1000;
            int hundredsDigit = (n / 100) % 10;
            int tensDigit = (n / 10) % 10;
            int lastDigit = n % 10;

            int firstNumber = thousandDigit * 10 + tensDigit;
            int secondNumber = hundredsDigit * 10 + lastDigit;

            int ceiling = firstNumber + secondNumber;
            int initialSecondNumber = secondNumber;

            List<int> numbersDivisibleBy12 = new List<int>();
            List<int> numbersDivisibleBy15 = new List<int>();

            while (firstNumber <= ceiling)
            {
                while (secondNumber <= ceiling)
                {
                    int newNumber = firstNumber * 100 + secondNumber;
                    if (newNumber % 12 == 0)
                    {
                        numbersDivisibleBy12.Add(newNumber);
                    }
                    if (newNumber % 15 == 0)
                    {
                        numbersDivisibleBy15.Add(newNumber);
                    }

                    secondNumber++;
                }

                firstNumber++;
                secondNumber = initialSecondNumber;
            }

            Console.WriteLine($"Dividing on 12: {string.Join(" ", numbersDivisibleBy12)}");
            Console.WriteLine($"Dividing on 15: {string.Join(" ", numbersDivisibleBy15)}");

            if (numbersDivisibleBy12.Count == numbersDivisibleBy15.Count)
            {
                Console.WriteLine("!!!BINGO!!!");
            }
            else
            {
                Console.WriteLine("NO BINGO!");
            }
        }
    }
}
