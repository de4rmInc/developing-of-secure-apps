using System;

namespace Laba2_hash_algorithms.Cryptography.HashAlgorithms
{
    public class Sha384 : HashAlgorithm
    {
        private Sha384Internal _internalAlgorithm;

        public Sha384()
        {
            _internalAlgorithm = new Sha384Internal();
        }

        public override byte[] CalculateHash(byte[] bytes)
        {
            return _internalAlgorithm.ComputeHash(bytes);
        }
        
        public override event RoundEventHandler RoundChanged
        {
            remove
            {
                Sha384Internal.RoundChanged -= value;
            }
            add
            {
                Sha384Internal.RoundChanged += value;
            }
        }
    }
}
