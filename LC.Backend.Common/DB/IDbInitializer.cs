using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public interface IDbInitializer
    {
        Task InitializeAsync();
    }
}
