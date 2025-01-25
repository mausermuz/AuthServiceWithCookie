namespace AuthServiceBulgakov.Domain.Exceptions
{
    public class FluentValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }
        public FluentValidationException(IReadOnlyDictionary<string, string[]> errors) : base("Ошибка валидации входящих данных")
            => Errors = errors;
    }
}