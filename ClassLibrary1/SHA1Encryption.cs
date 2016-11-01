using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    class SHA1Encryption : IEncryptionHelper
    {
        public string Encode(string input)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new ASCIIEncoding();
            var combined = encoder.GetBytes(input ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }
    }
}
