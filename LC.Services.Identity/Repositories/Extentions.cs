using LC.Services.Identity.Repositories.Encrypter;
using LC.Services.Identity.Repositories.Entities;
using System;

namespace LC.Services.Identity.Repositories
{
    public static class Extentions
    {
        public static void SetPassword(this User user, string password, IPasswordEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("Password can not be empty.");
            user.Salt = encrypter.GetSalt();
            user.Password = encrypter.GetHash(password, user.Salt);
        }

        public static bool ValidatePassword(this User user, string password, IPasswordEncrypter encrypter)
            => user.Password.Equals(encrypter.GetHash(password, user.Salt));
    }
}
