namespace LC.Services.Identity.Repositories.Encrypter
{
    public interface IPasswordEncrypter
    {
        string GetSalt();
        string GetHash(string value, string salt);
    }
}
