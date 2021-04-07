using System;

namespace FizzBuzz
{
    class Program
    {
        static void Main(string[] args)
        {

            var max = 100;
            for (int i = 1; i <= max; i++)
            {
                string output = i switch
                {
                    int fb when fb % 3 == 0 && fb % 5 == 0 => "fizzbuzz",
                    int f when f % 3 == 0 => "fizz",
                    int b when b % 5 == 0 => "buzz",
                    _ => i.ToString(),
                };

                Console.WriteLine(output);
            }
        }
    }
}
