using MediatR;

namespace Lemmo.WebApi.Application.Commands.Auth.Login
{
    public record LoginCommand(string PhoneNumber, string Password, string? Device) : IRequest<AuthResponse>;
}
