using System;

namespace LC.Backend.Common.Commands.Models
{
    public class CreateUser : ICommand
    {
        public Guid CorrelationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
