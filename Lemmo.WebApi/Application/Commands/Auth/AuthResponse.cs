namespace Lemmo.WebApi.Application.Commands.Auth
{
    public record AuthResponse(string AccessToken, string RefreshToken);
}
