using Lemmo.WebApi.Persistance.Auth.Interfaces;

namespace Lemmo.WebApi.Persistance.Auth.Implementations
{
    public class Hasher : IHasher
    {
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(password, hashedPassword);

        public string HashRefreshToken(string refreshToken) =>
            BCrypt.Net.BCrypt.HashPassword(refreshToken);

        public bool VerifyRefreshToken(string hashedRrefreshToken, string refreshToken) =>
            BCrypt.Net.BCrypt.Verify(refreshToken, hashedRrefreshToken);
    }
}
