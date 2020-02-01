using System;

namespace AwesomeNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = int.Parse(Console.ReadLine());
            int favoriteNumber = int.Parse(Console.ReadLine());
            int meetCinditions = 0;
            
            if(number % 2 != 0)
            {
                meetCinditions++;
            }
            if(number < 0)
            {
                meetCinditions++;
            }
            if(number % favoriteNumber == 0)
            {
                meetCinditions++; ;
            }

            string output = "boring";

            if(meetCinditions == 1)
            {
                output = "awesome";
            }
            else if(meetCinditions == 2)
            {
                output = "super awesome";
            }
            else if(meetCinditions == 3)
            {
                output = "super special awesome";
            }

            Console.WriteLine(output);
        }
    }
}
