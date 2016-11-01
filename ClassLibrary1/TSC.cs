using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    class TSC : IEncryptionHelper
    {
        public string Encode(string input)
        {
            var hash = System.Security.Cryptography.MD5.Create();
            var encoder = new ASCIIEncoding();
            var combined = encoder.GetBytes(input ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
