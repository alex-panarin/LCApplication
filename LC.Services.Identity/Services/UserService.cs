using LC.Backend.Common.Auth;
using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Operations;
using LC.Services.Identity.Operations;
using LC.Services.Identity.Repositories;
using LC.Services.Identity.Repositories.Encrypter;
using LC.Services.Identity.Repositories.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordEncrypter _encrypter;
        private readonly IJwtHandler _handler;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository,
            IPasswordEncrypter encrypter,
            IJwtHandler handler,
            ILogger logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _encrypter = encrypter ?? throw new ArgumentNullException(nameof(encrypter));
            _handler = handler ?? throw new ArgumentNullException(nameof(JwtHandler));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<bool>> CreateAsync(string email, string name, string password, Guid correlationId)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new LCException($"Email: {email}, is already in use");

            user = new User(email, name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);

            return await Task.FromResult(Result<bool>.Success(true, correlationId));
        }

        public async Task<Result<JsonWebToken>> LoginAsync(Authenticate authenticate, Guid correlationId)
        {
            var operation = new LoginOperation(_logger, _userRepository, _encrypter, _handler);
            var result = await operation.ExecuteAsync(authenticate, correlationId);
            if (result?.IsSuccess == false)
                return result ?? Result<JsonWebToken>.Error($"Error in {nameof(LoginOperation)}", correlationId: correlationId);

            return result;
        }
    }
}
