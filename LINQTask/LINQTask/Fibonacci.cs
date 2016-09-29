using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace LINQTask
{
    class Fibonacci
    {        

        public BigInteger[] CreateArray(int n)
        {
            BigInteger a = 0, b = 1;
            BigInteger[] array = new BigInteger[n];
            for(int i = 0; i < n; i = i+2)
            {
                array[i] = a;
                array[i + 1] = b;
                a = a + b;
                b = b + a;
            }
            return array;
        }
    }
}
