using System;

namespace Laba2_hash_algorithms.HashAlgorithms
{
    public class Sha384
    {

        public byte[] CalculateHash(byte[] bytes)
        {
            var sha = new Sha384Internal();
            var hash = sha.ComputeHash(bytes);

            return hash;
        }
        
    }
}
