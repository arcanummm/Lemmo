namespace Lemmo.WebApi.Persistance.Auth.Interfaces
{
    public interface IHasher
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string hashedPassword);

        string HashRefreshToken(string refreshToken);

        bool VerifyRefreshToken(string hashedRrefreshToken, string refreshToken);
    }
}
