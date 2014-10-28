using System;
using System.Numerics;

namespace Laba2_hash_algorithms.HashAlgorithms
{
    public static class Sha384Functions
    {
        //word length
        private const int _w_64 = 64;
        private static readonly UInt64 module64;

        static Sha384Functions()
        {
            module64 = (UInt64)Math.Pow(2, _w_64);
        }
        
        public static UInt64 Ch(UInt64 x, UInt64 y, UInt64 z)
        {
            return ((x & y) ^ ((x ^ 0xffffffffffffffff) & z));
        }

        public static UInt64 Maj(UInt64 x, UInt64 y, UInt64 z)
        {
            return ((x & y) ^ (x & z) ^ (y & z));
        }

        public static UInt64 Parity(UInt64 x, UInt64 y, UInt64 z)
        {
            return x ^ y ^ z;
        }

        public static UInt64 F(UInt64 t, UInt64 x, UInt64 y, UInt64 z)
        {
            UInt64 result = 0;
            if (t <= 19)
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
        public static UInt64 SHR(UInt64 x, int n)
        {
            return x >> n;
        }

        /// <summary>
        /// Circle rotate to the right
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static UInt64 ROTR(UInt64 x, int n)
        {
            return (x >> n) | (x << (_w_64 - n));
        }

        /// <summary>
        /// Cicle rotate to the left
        /// </summary>
        /// <param name="x"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static UInt64 ROTL(UInt64 x, int n)
        {
            return (x << n) | (x >> (_w_64 - n));
        }

        public static UInt64 Sigm0to512(UInt64 x)
        {
            return ROTR(x, 28) ^ ROTR(x, 34) ^ ROTR(x, 39);
        }

        public static UInt64 Sigm1to512(UInt64 x)
        {
            return ROTR(x, 14) ^ ROTR(x, 18) ^ ROTR(x, 41);
        }

        public static UInt64 sigm0to512(UInt64 x)
        {
            return ROTR(x, 1) ^ ROTR(x, 8) ^ SHR(x, 7);
        }

        public static UInt64 sigm1to512(UInt64 x)
        {
            return ROTR(x, 19) ^ ROTR(x, 61) ^ SHR(x, 6);
        }
        
    }
}
