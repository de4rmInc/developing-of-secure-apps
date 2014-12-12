using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.Cryptography.HashAlgorithms
{
    public abstract class HashAlgorithm : IHashAlgorithm
    {
        public abstract byte[] CalculateHash(byte[] bytes);

        public static HashAlgorithm Default
        {
            get
            {
                return _default;
            }
        }

        private static HashAlgorithm _default = new Sha384();
    }
}
