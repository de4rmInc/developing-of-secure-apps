using System;
using System.Windows.Markup;

namespace HashAlgorithms.HashAlgorithms
{
    public static class ShaFunctions
    {
        private static readonly int module32;
        private static readonly long module64;

        static ShaFunctions()
        {
            module32 = (int)Math.Pow(2, 32);
            module64 = (long)Math.Pow(2, 64);
        }

        public static int Ch(int x, int y, int z)
        {
            return (x & y) ^ (Invert(x) & z);
        }

        public static int Parity(int x, int y, int z)
        {
            return x ^ y ^ z;
        }

        public static int Maj(int x, int y, int z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        public static int F(int t, int x, int y, int z)
        {
            int result = 0;
            if (t >= 0 && t <= 19)
            {
                result = Ch(x, y, z);
            }
            else if (t >= 20 && t <= 39)
            {
                result = Parity(x, y, z);
            }
            else if (t >= 40 && t <= 59)
            {
                result = Maj(x, y, z);
            }
            else if (t >= 60 && t <= 79)
            {
                result = Parity(x, y, z);
            }
            return result;
        }

        /// <summary>
        /// Shift to the right
        /// </summary>
        /// <param name="x">number</param>
        /// <param name="n">shift</param>
        /// <returns></returns>
        public static int SHR(int x, int n)
        {
            return x >> n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ROTR(int x, int n)
        {
            return 0;
        }

        private static int Invert(int x)
        {
            var n = Math.Log(x, 2);
            var ceiledN = Math.Ceiling(n);
            var moduleN = (int)Math.Pow(2, ceiledN);

            if (Math.Abs(n - ceiledN) > 0)
            {
                return x ^ (moduleN - 1);
            }
            return moduleN - 1;
        }
    }
}
