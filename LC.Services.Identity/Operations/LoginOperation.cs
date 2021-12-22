using LC.Backend.Common.Auth;
using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.Operations;
using LC.Services.Identity.Repositories;
using LC.Services.Identity.Repositories.Encrypter;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Operations
{
    public class LoginOperation : OperationBase<Authenticate, JsonWebToken>
    {
        private IUserRepository _userRepository;
        private readonly IPasswordEncrypter _encrypter;
        private readonly IJwtHandler _handler;

        protected override string OperationName => nameof(LoginOperation);

        public LoginOperation(ILogger logger,
            IUserRepository userRepository,
            IPasswordEncrypter encrypter,
            IJwtHandler handler)
            :base (logger)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _handler = handler;
        }
        protected override async Task<Result<JsonWebToken>> ExecuteImplAsync(Authenticate authenticate)
        {
            var user = await _userRepository.GetAsync(authenticate.Email);
            if (user == null)
                throw new LCException("Invalid user");

            if (!user.ValidatePassword(authenticate.Password, _encrypter))
                throw new LCException("Invalid credential");

            return await Task.FromResult(Result<JsonWebToken>.Success(_handler.Create(user.Id)));
        }

    }
}
