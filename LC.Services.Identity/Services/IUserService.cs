using System.Threading.Tasks;

namespace LC.Services.Identity.Services
{
    public interface IUserService
    {
        Task CreateAsync(string email, string name, string pass);
    }
}
