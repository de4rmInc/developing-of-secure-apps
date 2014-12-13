using Laba2_hash_algorithms.Cryptography;
using Laba2_hash_algorithms.Cryptography.HashAlgorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba2_hash_algorithms.Models
{
    public class CryptographyModel
    {
        public string FilePath { get; set; }
        private HashAlgorithm _hashAlgorithm;

        public byte[] HashCode { get; private set; }
        public byte[] FileBytes { get; private set; }
        public byte[] EncryptedFileBytes { get; private set; }
        public string Text { get; private set; }
        public bool IsTxt { get; private set; }

        public CryptographyModel(string path)
            : this(path, HashAlgorithm.Default)
        {
        }

        public CryptographyModel(string path, HashAlgorithm hashAlgorithm)
        {
            FilePath = path;
            _hashAlgorithm = hashAlgorithm;
        }

        public byte[] CalculateFileHashCode()
        {
            if (IsTxt = FilePath.EndsWith(".txt"))
            {
                Text = File.ReadAllText(FilePath);
            }

            FileBytes = ReadFile(FilePath);

            HashCode = CalculateHash(FileBytes);

            return HashCode;
        }

        public byte[] CalculateHash(byte[] bytes)
        {
            return _hashAlgorithm.CalculateHash(bytes);
        }

        public byte[] Encrypt()
        {
            if (FileBytes == null || FileBytes.Length == 0)
            {
                FileBytes = ReadFile(FilePath);
            }

            EncryptedFileBytes = ProtectData.EncryptData(FileBytes);

            return EncryptedFileBytes;
        }

        private byte[] ReadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("FilePath");
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);

            return fileBytes;
        }

        public event RoundEventHandler RoundChanged
        {
            remove
            {
                _hashAlgorithm.RoundChanged -= value;
            }
            add
            {
                _hashAlgorithm.RoundChanged += value;
            }
        }
    }
}
