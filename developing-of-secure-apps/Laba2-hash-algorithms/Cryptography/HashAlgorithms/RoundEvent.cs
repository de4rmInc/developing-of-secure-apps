using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.Cryptography.HashAlgorithms
{

    public delegate void RoundEventHandler(RoundEventArgs e);

    public class RoundEventArgs : EventArgs
    {
        private readonly int _round;
        private readonly int _bitsChangedCount;
        private readonly bool _lastRound;

        public RoundEventArgs(int round, int bitsChangedCount, bool lastRound = false)
        {
            _round = round;
            _bitsChangedCount = bitsChangedCount;
            _lastRound = lastRound;
        }

        public int Round
        {
            get { return _round; }
        }
        public int BitsChangedCount
        {
            get { return _bitsChangedCount; }
        }
        public bool LastRound
        {
            get { return _lastRound; }
        }

    }
}
