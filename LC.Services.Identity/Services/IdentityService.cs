using Grpc.Core;
using LC.Backend.Common.Commands.Models;
using System;
using System.Threading.Tasks;

namespace LC.Services.Identity.Services
{
    public class IdentityService : Identity.IdentityBase
    {
        private readonly IUserService _userService;
        public IdentityService(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        public override async Task<AuthResponce> Login(AuthRequest request, ServerCallContext context)
        {
            var result = await _userService.LoginAsync(new Authenticate { Email = request.Email, Password = request.Password }, Guid.NewGuid());
            return new AuthResponce
            {
                IsSuccess = result.IsSuccess,
                ErrorMessage = result.ErrorMessages,
                Expires = result.IsSuccess ? result.Data.Expires : 0,
                Token = result.Data?.Token
            };
        }

        public override async Task<CreateResponse> Create(CreateRequest request, ServerCallContext context)
        {
            var result = await _userService.CreateAsync(request.Email, request.Name, request.Password, Guid.NewGuid());
            return new CreateResponse { IsSuccess = result.IsSuccess, ErrorMessage = result.ErrorMessages };
        }
    }
}
