using System;
using System.Security.Cryptography;

namespace LC.Services.Identity.Repositories.Encrypter
{
    public class PasswordEcnrypter : IPasswordEncrypter
    {
        private static readonly int SaltSize = 40;
        public string GetHash(string value, string salt)
        {
            var hash = new Rfc2898DeriveBytes(value, GetBytes(salt),10000);
            return Convert.ToBase64String(hash.GetBytes(SaltSize));
        }

        private byte[] GetBytes(string salt)
        {
            var bytes = new byte[salt.Length * sizeof(char)];
            Buffer.BlockCopy(salt.ToCharArray(), 0, bytes, 0, salt.Length);
            return bytes;
        }

        public string GetSalt()
        {
            var rnd = RandomNumberGenerator.Create();
            var salts = new byte[SaltSize];
            rnd.GetBytes(salts);

            return Convert.ToBase64String(salts);
        }
    }
}
