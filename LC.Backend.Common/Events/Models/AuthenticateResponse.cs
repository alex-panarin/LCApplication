using LC.Backend.Common.Auth;

namespace LC.Backend.Common.Events.Models
{
    public class AuthenticateResponse
    {
        public JsonWebToken Token { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
