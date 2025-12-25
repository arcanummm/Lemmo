using Lemmo.Domain.Common;

namespace Lemmo.Domain.Users.Exceptions
{
    public sealed class InvalidTelegramIdException(string reason) : DomainException($"Invalid telegram id: {reason}")
    {
        public string Reason { get; } = reason;
    }
}
