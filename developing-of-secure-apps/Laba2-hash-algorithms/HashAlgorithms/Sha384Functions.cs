using System;
using System.Numerics;

namespace Laba2_hash_algorithms.HashAlgorithms
{
    public static class Sha384Functions
    {
        //word length
        private const int _w_64 = 64;
        private static readonly BigInteger module64;

        static Sha384Functions()
        {
            module64 = BigInteger.Pow(2, _w_64);
        }

        public static BigInteger Ch(BigInteger x, BigInteger y, BigInteger z)
        {
            return (x & y) ^ (Invert(x) & z);
        }

        public static BigInteger Parity(BigInteger x, BigInteger y, BigInteger z)
        {
            return x ^ y ^ z;
        }

        public static BigInteger Maj(BigInteger x, BigInteger y, BigInteger z)
        {
            return (x & y) ^ (x & z) ^ (y & z);
        }

        public static BigInteger F(BigInteger t, BigInteger x, BigInteger y, BigInteger z)
        {
            BigInteger result = 0;
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
        public static BigInteger SHR(BigInteger x, int n)
        {
            return x >> n;
        }

        /// <summary>
        /// Cicle rotate to the right
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger ROTR(BigInteger x, int n)
        {
            return (x >> n) | (x << (_w_64 - n));
        }

        /// <summary>
        /// Cicle rotate to the left
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static BigInteger ROTL(BigInteger x, int n)
        {
            return (x << n) | (x >> (_w_64 - n));
        }

        public static BigInteger Sum0to512(BigInteger x)
        {
            return ROTR(x, 28) ^ ROTR(x, 34) ^ ROTR(x, 39);
        }

        public static BigInteger Sum1to512(BigInteger x)
        {
            return ROTR(x, 14) ^ ROTR(x, 18) ^ ROTR(x, 41);
        }

        public static BigInteger Sigm0to512(BigInteger x)
        {
            return ROTR(x, 1) ^ ROTR(x, 8) ^ SHR(x, 7);
        }

        public static BigInteger Sigm1to512(BigInteger x)
        {
            return ROTR(x, 19) ^ ROTR(x, 61) ^SHR(x, 6);
        }

        private static UInt64 RotateRight(UInt64 x, int n)
        {
            return (((x) >> (n)) | ((x) << (64 - (n))));
        }

        private static UInt64 Ch(UInt64 x, UInt64 y, UInt64 z)
        {
            return ((x & y) ^ ((x ^ 0xffffffffffffffff) & z));
        }

        private static UInt64 Maj(UInt64 x, UInt64 y, UInt64 z)
        {
            return ((x & y) ^ (x & z) ^ (y & z));
        }

        private static UInt64 Sigma_0(UInt64 x)
        {
            return (RotateRight(x, 28) ^ RotateRight(x, 34) ^ RotateRight(x, 39));
        }

        private static UInt64 Sigma_1(UInt64 x)
        {
            return (RotateRight(x, 14) ^ RotateRight(x, 18) ^ RotateRight(x, 41));
        }

        private static UInt64 sigma_0(UInt64 x)
        {
            return (RotateRight(x, 1) ^ RotateRight(x, 8) ^ (x >> 7));
        }

        private static UInt64 sigma_1(UInt64 x)
        {
            return (RotateRight(x, 19) ^ RotateRight(x, 61) ^ (x >> 6));
        }

        private static BigInteger Invert(BigInteger x)
        {
            if (x == 0) return 1;
            if (x == 1) return 0;
            if (x < 0) return 0;

            var n = BigInteger.Log(x, 2);
            var ceiledN = (int)n;
            var moduleN = BigInteger.Pow(2, ceiledN);

            if (Math.Abs(n - ceiledN) > 0)
            {
                moduleN = BigInteger.Pow(2, ceiledN + 1);
                return x ^ (moduleN - 1);
            }
            return moduleN - 1;
        }

        private static BigInteger DivRem(BigInteger a, BigInteger n)
        {
            return BigInteger.Remainder(a, n);
        }
    }
}
