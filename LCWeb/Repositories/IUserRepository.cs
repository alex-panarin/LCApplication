namespace LCWeb.Repositories
{
    public interface IUserRepository
    {
        string[] UserNames { get; } 
        string[] UserRoles { get; }
    }
}
