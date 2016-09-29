using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace LINQTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Fibonacci fibonacci = new Fibonacci();
            BigInteger[] array = fibonacci.CreateArray(200);
            foreach (BigInteger a in array)
            {
                Console.Write(a + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Prime numbers "+array.FindPrimesCount());
            Console.WriteLine("Special numbers " + array.FindSpecialNumbers());
            Console.WriteLine("Has numbers divided by five " + array.HasNumberDividedByFive());
            BigInteger[] newFAray = array.SqrtWithDigitTwo();
            foreach(BigInteger num in newFAray)
            {
                Console.WriteLine(num);
            }
            Console.ReadKey();
        }
    }
}
