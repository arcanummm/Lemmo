using Lemmo.WebApi.Persistance.Auth.Interfaces;
using MediatR;

namespace Lemmo.WebApi.Application.Commands.Auth.Login
{
    public class LoginCommandHandler(IAuthManager authManager) : IRequestHandler<LoginCommand, AuthResponse>
    {
        public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken ct)
        {
            var result = await authManager.Login(request.PhoneNumber, request.Password, request.Device, ct);

            if(result.IsSuccessful)
            {
                return new AuthResponse(result.AccessToken!, result.RefreshToken!);
            }

            return new AuthResponse("", "");
        }
    }
}
