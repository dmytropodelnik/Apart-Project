using CloneBookingAPI.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace CloneBookingAPI.Services.Generators
{
    public class SaltGenerator : BaseGenerator, IGenerator
    {
        private byte[] _salt = new byte[128 / 8];
        // private readonly string _salt = "saltforpassapartproject321WE";
        public string GenerateCode(string str)
        {
            try
            {
                byte[] salt = new byte[128 / 8];
                // generate a 128 - bit salt using a cryptographically strong random sequence of nonzero values
                using (var rngCsp = RandomNumberGenerator.Create())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                _salt = salt;

                GeneratePassHash(str, Convert.ToBase64String(_salt));

                return _code;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
        public string GeneratePassHash(string password, string saltstr)
        {
            try
            {
                byte[] salt = Encoding.Default.GetBytes(saltstr);
                // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                _code = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,  // Encoding.Default.GetBytes(_salt),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return _code;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }

        public string Salt
        {
            get => Convert.ToBase64String(_salt);
        }
    }
}
