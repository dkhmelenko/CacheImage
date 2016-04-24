using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace CacheImage
{
    /// <summary>
    /// Provides encryption utilities
    /// </summary>
    internal static class EncryptionUtils
    {
        private static string GetHash(string algoritm, string s)
        {
            HashAlgorithmProvider alg = HashAlgorithmProvider.OpenAlgorithm(algoritm);
            IBuffer buff = CryptographicBuffer.ConvertStringToBinary(s, BinaryStringEncoding.Utf8);
            var hashed = alg.HashData(buff);
            var res = CryptographicBuffer.EncodeToHexString(hashed);
            return res;
        }

        public static string GetSha1(string s)
        {
            return GetHash(HashAlgorithmNames.Sha1, s);
        }

        public static string GetSha256(string s)
        {
            return GetHash(HashAlgorithmNames.Sha256, s);
        }

        public static string GetSha384(string s)
        {
            return GetHash(HashAlgorithmNames.Sha384, s);
        }

        public static string GetSha512(string s)
        {
            return GetHash(HashAlgorithmNames.Sha512, s);
        }

        public static string GetMD5(string s)
        {
            return GetHash(HashAlgorithmNames.Md5, s);
        }
    }
}
