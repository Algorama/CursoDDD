using System;
using System.Security.Cryptography;
using System.Text;

namespace Kernel.Domain.Model.Helpers
{
    public class CryptoHelper
    {
        public static string ComputeHashMd5(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var algorithm = new MD5CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

        public static string ComputeHashSha256(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            var algorithm = new SHA256CryptoServiceProvider();
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}