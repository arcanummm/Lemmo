using Lemmo.Domain.Common;
using Lemmo.Domain.Users.Enums;
using Lemmo.Domain.Users.Events;
using Lemmo.Domain.Users.Exceptions;
using Lemmo.Domain.Users.ValueObjects;

namespace Lemmo.Domain.Users
{
    public class User : AggregateRoot
    {
        private const int MinNameLength = 2;
        private const int MaxNameLength = 50;

        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; init; }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public PhoneNumber Phone { get; private set; }

        public TelegramId TelegramId { get; private set; }

        private User() { }

        public static User Register(TelegramId telegramId, string passwordHash, PhoneNumber phone, UserRole role = UserRole.User)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new InvalidPasswordHashException("Password hash is required");

            var user = new User
            {
                TelegramId = telegramId,
                PasswordHash = passwordHash,
                Role = role,
                Phone = phone,
            };

            return user;
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new InvalidPasswordHashException("Password hash is required");

            PasswordHash = newPasswordHash;
            UpdateTimestamp();

            RaiseDomainEvent(new UserPasswordChangedEvent(Id));
        }

        public void UpdateProfile(string? firstName = null, string? lastName = null)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                if (firstName.Trim().Length is < MinNameLength or > MaxNameLength)
                    throw new DomainException($"First name must be between {MinNameLength} and {MaxNameLength} characters");
                FirstName = firstName.Trim();
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                if (lastName.Trim().Length is < MinNameLength or > MaxNameLength)
                    throw new DomainException($"Last name must be between {MinNameLength} and {MaxNameLength} characters");
                LastName = lastName.Trim();
            }

            UpdateTimestamp();
        }
    }
}
