using System.Net.Http;

namespace LCWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _client;

        public UserRepository(HttpClient client)
        {
            _client = client;
        }

        public string[] UserNames { get => new[] { "Vasia", "Petia" }; }// set => _userNames = value; }
        public string[] UserRoles { get => new[] { "Admin", "Cutter" }; }// set => _userRoles = value; }
    }
}
