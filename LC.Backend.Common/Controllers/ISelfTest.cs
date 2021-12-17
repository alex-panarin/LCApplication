using System.Threading.Tasks;

namespace LC.Backend.Common.Controllers
{
    public interface ISelfTest
    {
        Task<string> Ping();
    }
}
