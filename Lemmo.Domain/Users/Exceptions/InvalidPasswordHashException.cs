using Lemmo.Domain.Common;

namespace Lemmo.Domain.Users.Exceptions
{
    public sealed class InvalidPasswordHashException(string reason) : DomainException($"Invalid password hash: {reason}")
    {
        public string Reason { get; } = reason;
    }
}
