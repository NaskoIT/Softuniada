using System;
using System.Text;

namespace DuplicatedLetters
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder s = new StringBuilder(Console.ReadLine());
            int operationsCount = 0;

            for (int i = 0; i < s.Length - 1; i++)
            {
                if(s[i] == s[i + 1])
                {
                    s.Remove(i, 2);
                    i = Math.Max(-1, i - 2);
                    operationsCount++;
                }
            }

            string result = s.ToString().Trim();
            Console.WriteLine(result == string.Empty ? "Empty String" : result);
            Console.WriteLine($"{operationsCount} operations");
        }
    }
}
