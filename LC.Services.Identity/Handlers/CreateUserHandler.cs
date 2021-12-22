using LC.Backend.Common.Commands;
using LC.Backend.Common.Commands.Models;
using LC.Services.Identity.Services;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Handlers
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService _userService;

        public CreateUserHandler(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public async Task HandleAsync(CreateUser command)
        {
            await _userService.CreateAsync(command.Email, command.Name, command.Password, Guid.NewGuid());
        }
    }
}
