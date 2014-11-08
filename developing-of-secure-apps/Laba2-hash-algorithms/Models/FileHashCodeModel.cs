using Laba2_hash_algorithms.HashAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.Models
{
    public class FileHashCodeModel
    {
        public string FilePath { get; set; }
        private HashAlgorithm _hashAlgorithm;

        public byte[] HashCode { get; private set; }
        public byte[] FileBytes { get; private set; }
        public string Text { get; private set; }
        public bool IsTxt { get; private set; }

        public FileHashCodeModel(string path)
            : this(path, HashAlgorithm.Default)
        {
        }

        public FileHashCodeModel(string path, HashAlgorithm hashAlgorithm)
        {
            FilePath = path;
            _hashAlgorithm = hashAlgorithm;
        }

        public byte[] CalculateFileHashCode()
        {
            if (IsTxt = FilePath.EndsWith(".txt"))
            {
                Text = File.ReadAllText(FilePath);
                FileBytes = Encoding.Default.GetBytes(Text);
            }
            else
            {
                FileBytes = File.ReadAllBytes(FilePath);
            }

            HashCode = _hashAlgorithm.CalculateHash(FileBytes);

            return HashCode;
        }
    }
}
