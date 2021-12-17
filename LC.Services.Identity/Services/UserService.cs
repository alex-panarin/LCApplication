using System.Threading.Tasks;

namespace LC.Services.Identity.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }
        public Task CreateAsync(string email, string name, string pass)
        {
            throw new System.NotImplementedException();
        }
    }
}
