namespace LC.Backend.Common.Events.Models
{
    public class AuthenticateRequest : IEvent
    {
        public string email { get; set; }
        public string password { get; set; }

    }
}
