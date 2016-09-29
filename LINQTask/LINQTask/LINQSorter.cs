using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace LINQTask
{
    static class LINQSorter
    {
        public static int FindPrimesCount(this BigInteger[] array)
        {
            BigInteger[] a = array.Where(x => x%2!=0 || x==1||x==0 || x == 2 ).
                Where(x => x % 3 != 0 || x == 1 || x == 0 || x == 2).
                Where(x => x % 5 != 0 || x == 1 || x == 0 || x == 2).
                Where(x => x % 7 != 0 || x == 1 || x == 0 || x == 2).
                Where(x => x % 11 != 0 || x == 1 || x == 0 || x == 2)
                .ToArray();
            return a.Length;
        }

        public static int FindSpecialNumbers(this BigInteger[] array)
        {

            BigInteger[] a = array.Where(x => x!=0).Where(x => x % x.FindSumOfDigits() == 0).ToArray();
            return a.Length ;
        }

        public static bool HasNumberDividedByFive(this BigInteger[] array)
        {
            BigInteger[] a = array.Where(x => x % 5 == 0).ToArray();
            if(a.Length > 0)
            {
                return true;
            }
            return false;
        }

        public static BigInteger[] SqrtWithDigitTwo(this BigInteger[] array)
        {
            BigInteger[] a = array.Where(x => x.ToString().Contains("2")).Select(x => (BigInteger)Math.Floor(Math.Sqrt((double)x))).ToArray();
            return a;
        }

        public static BigInteger[] SortBySecondDigit(this BigInteger[] array)
        {
            var a = array.OrderBy(x => )
        }

        public static void FindSumOfDigits(this BigInteger number)
        {
            int num = 0;
            while (number > 10)
            {
                num = (int)number % 10;
                number /= 10;
            }
            Console.WriteLine(num);
        }

        public static int FindSumOfDigits(this BigInteger number)
        {
            BigInteger sum = 0;
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return (int)sum;
        }
    }

    
}
