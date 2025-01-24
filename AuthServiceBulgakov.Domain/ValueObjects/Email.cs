using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AuthServiceBulgakov.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private const string EMAIL_PATTERN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        public string Value { get; }

        protected Email() { }
        protected Email(string email)
        {
            Value = email;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>("Пустой email");

            if (!Regex.IsMatch(email, EMAIL_PATTERN))
                return Result.Failure<Email>("Введенная строка не является электронной почтой");

            return new Email(email);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
