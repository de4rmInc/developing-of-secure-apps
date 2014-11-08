using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.HashAlgorithms
{
    public interface IHashAlgorithm
    {
        byte[] CalculateHash(byte[] bytes);
    }
}
