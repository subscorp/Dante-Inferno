using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void initialize(out int x)
        {
            x = 0;
        }

        static void increment(ref int x)
        {
            x++;
        }

        static void Main(string[] args)
        {
            int num1, num2;

            do
            {
                Console.Write("Enter first number: ");
            } while (!int.TryParse(Console.ReadLine(), out num1));

            do
            {
                Console.Write("Enter second number: ");

            } while (!int.TryParse(Console.ReadLine(), out num2));

            Console.WriteLine($"sum is: {num1 + num2}");
        }
    }
}
