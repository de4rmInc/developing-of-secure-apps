using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = 1;
            Console.WriteLine(x);
            Console.WriteLine();
            Console.WriteLine(Invert4(x));
            Console.WriteLine(Invert4_big(x));
            
            Console.WriteLine();

        }

        static int Ch(int x, int y, int z)
        {

            return (x & y) ^ (Invert1(x) & z);
        }

        private static int Invert1(int x)
        {
            var module32 = (int)Math.Pow(2, 32);

            return (x ^ (module32 - 1));
        }

        private static int Invert2(int x)
        {
            return ~x;
        }

        private static int Invert3(int x)
        {
            var module32 = (int)Math.Pow(2, 32);

            return ~x & (module32 - 1);
        }

        //Наиболее верный вариант
        private static int Invert4(int x)
        {
            if (x == 0) return 1;
            if (x < 0) return 0;

            var n = Math.Log(x, 2);
            var ceiledN = Math.Ceiling(n);
            var moduleN = (int)Math.Pow(2, ceiledN);

            if (Math.Abs(n - ceiledN) > 0)
            {
                return x ^ (moduleN - 1);
            }
            return moduleN - 1;
        }

        private static BigInteger Invert4_big(BigInteger x)
        {
            if (x == 0) return 1;
            if (x == 1) return 0;
            if (x < 0) return 0;

            var n = BigInteger.Log(x, 2);
            var ceiledN = (int) n;
            var moduleN = BigInteger.Pow(2, ceiledN);

            if (Math.Abs(n - ceiledN) > 0)
            {
                moduleN = BigInteger.Pow(2, ceiledN + 1);
                return x ^ (moduleN - 1);
            }
            return moduleN - 1;
        }

        private static uint Invert5(uint x)
        {
            uint mask = 0;

            for (int i = 1; i <= 16; i *= 2)
            {
                mask |= mask >> i;
            }

            return mask & (~x);
        }
    }
}
