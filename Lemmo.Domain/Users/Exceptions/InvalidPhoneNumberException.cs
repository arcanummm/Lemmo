using Lemmo.Domain.Common;

namespace Lemmo.Domain.Users.Exceptions
{
    public sealed class InvalidPhoneNumberException(string reason) : DomainException($"Invalid phone number: {reason}")
    {
        public string Reason { get; } = reason;
    }
}
