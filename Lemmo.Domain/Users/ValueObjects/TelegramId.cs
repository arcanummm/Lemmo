using Lemmo.Domain.Users.Exceptions;

namespace Lemmo.Domain.Users.ValueObjects
{
    public sealed class TelegramId
    {
        public long Value { get; }

        private TelegramId(long value)
        {
            Value = value;
        }

        public static TelegramId Create(long id)
        {
            if (id <= 0)
                throw new InvalidTelegramIdException("Telegram ID must be a positive number");

            return new TelegramId(id);
        }

        public override string ToString() => Value.ToString();

        public override bool Equals(object? obj)
            => obj is TelegramId other && Value == other.Value;

        public static bool operator ==(TelegramId? a, TelegramId? b)
            => Equals(a, b);

        public static bool operator !=(TelegramId? a, TelegramId? b)
            => !Equals(a, b);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
