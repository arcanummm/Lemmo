using Lemmo.Domain.Users.Exceptions;

namespace Lemmo.Domain.Users.ValueObjects
{
    public sealed class PhoneNumber
    {
        private const int MinDigits = 8;
        private const int MaxDigits = 15;

        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new InvalidPhoneNumberException("Phone number is required");

            var normalized = Normalize(input);

            return new PhoneNumber(normalized);
        }

        private static string Normalize(string input)
        {
            input = input.Trim();

            if (!input.StartsWith('+'))
                throw new InvalidPhoneNumberException("Phone number must start with '+'");

            Span<char> buffer = stackalloc char[input.Length];
            buffer[0] = '+';

            int index = 1;
            int digitsCount = 0;

            if (input.Length < MinDigits + 1 || input.Length > MaxDigits + 1)
                throw new InvalidPhoneNumberException("Phone number has invalid length");

            for (int i = 1; i < input.Length; i++)
            {
                if (!char.IsDigit(input[i]))
                    throw new InvalidPhoneNumberException("Phone number must contain digits only");

                buffer[index++] = input[i];
                digitsCount++;
            }

            return new string(buffer[..index]);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj)
        {
            if (obj is not PhoneNumber other)
                return false;

            return Value == other.Value;
        }

        public static bool operator ==(PhoneNumber? a, PhoneNumber? b)
            => Equals(a, b);

        public static bool operator !=(PhoneNumber? a, PhoneNumber? b)
            => !Equals(a, b);

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
