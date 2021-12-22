using LC.Backend.Common.DB;
using System;

namespace LC.Services.Identity.Repositories.Entities
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; protected set; }

        protected User()
        {
        }

        public User(string email, string name)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
