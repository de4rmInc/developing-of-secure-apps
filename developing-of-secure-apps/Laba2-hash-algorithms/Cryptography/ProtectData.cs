using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Laba2_hash_algorithms.Cryptography
{
    internal static class ProtectData
    {
        private static readonly byte[] DefaultEntropy = { 65, 82, 155, 71, 86, 44, 205, 128, 221, 11, 85, 144, 250, 84, 155, 183 };

        #region not Secure strings
        public static string EncryptData(string data)
        {
            return EncryptData(data, DefaultEntropy, DataProtectionScope.LocalMachine);
        }
        public static byte[] EncryptData(byte[] data)
        {
            return EncryptData(data, DefaultEntropy, DataProtectionScope.LocalMachine);
        }
        public static string DecryptData(string encryptedData)
        {
            return DecryptData(encryptedData, DefaultEntropy, DataProtectionScope.LocalMachine);
        }

        public static string EncryptData(string data, byte[] entropy, DataProtectionScope scope)
        {
            var preparedToEncrypt = GetBytes(data);

            return ConvertToString(EncryptData(preparedToEncrypt, entropy, scope));
        }

        public static string DecryptData(string encryptedData, byte[] entropy, DataProtectionScope scope)
        {
            var prepareToDecrypt = ConvertFromString(encryptedData);

            return GetString(DecryptData(prepareToDecrypt, entropy, scope));
        }
        #endregion

        #region Secure strings
        public static string EncryptDataS(SecureString data)
        {
            return EncryptDataS(data, DefaultEntropy, DataProtectionScope.LocalMachine);
        }

        public static SecureString DecryptDataS(string encryptedData)
        {
            return DecryptDataS(encryptedData, DefaultEntropy, DataProtectionScope.LocalMachine);
        }

        public static string EncryptDataS(SecureString data, byte[] entropy, DataProtectionScope scope)
        {
            var preparedToEncrypt = PrepareEncryptingString(data);

            return ConvertToString(EncryptData(preparedToEncrypt, entropy, scope));
        }

        public static SecureString DecryptDataS(string encryptedData, byte[] entropy, DataProtectionScope scope)
        {
            var prepareToDecrypt = ConvertFromString(encryptedData);

            return PrepareDecryptedString(DecryptData(prepareToDecrypt, entropy, scope));
        }
        #endregion

        #region private methods

        private static byte[] GetBytes(string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }

        private static string GetString(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }

        private static byte[] PrepareEncryptingString(SecureString str)
        {
            return GetBytes(ToInsecureString(str));
        }

        private static SecureString PrepareDecryptedString(byte[] stringBuffer)
        {
            return ToSecureString(GetString(stringBuffer));
        }

        private static byte[] ConvertFromString(string str)
        {
            return Convert.FromBase64String(str);
        }

        private static string ConvertToString(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        #endregion

        public static SecureString ToSecureString(string str)
        {
            SecureString secureString = new SecureString();
            foreach (char c in str)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        public static string ToInsecureString(SecureString secureString)
        {
            string str = string.Empty;
            IntPtr ptr = Marshal.SecureStringToBSTR(secureString);
            try
            {
                str = Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
            return str;
        }

        public static byte[] EncryptData(byte[] data, byte[] entropy, DataProtectionScope scope)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (data.Length <= 0)
                throw new ArgumentException("data");
            if (entropy == null)
                throw new ArgumentNullException("entropy");
            if (entropy.Length <= 0)
                throw new ArgumentException("entropy");

            // Encrypt the data in memory. The result is stored in the same same array as the original data.
            byte[] encryptedData = ProtectedData.Protect(data, entropy, scope);

            return encryptedData;
        }

        public static byte[] DecryptData(byte[] encryptedData, byte[] entropy, DataProtectionScope scope)
        {
            if (encryptedData == null)
                throw new ArgumentNullException("encryptedData");
            if (encryptedData.Length <= 0)
                throw new ArgumentException("Length");
            if (entropy == null)
                throw new ArgumentNullException("entropy");
            if (entropy.Length <= 0)
                throw new ArgumentException("entropy");

            byte[] outBuffer = ProtectedData.Unprotect(encryptedData, entropy, scope);

            return outBuffer;
        }

        public static byte[] CreateRandomEntropy()
        {
            // Create a byte array to hold the random value.
            byte[] entropy = new byte[16];

            // Create a new instance of the RNGCryptoServiceProvider.
            // Fill the array with a random value.
            new RNGCryptoServiceProvider().GetBytes(entropy);

            // Return the array.
            return entropy;
        }

    }
}