namespace AuthServiceBulgakov.Domain.Exceptions
{
    public class ValidationApplicationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }
        public ValidationApplicationException(IReadOnlyDictionary<string, string[]> errors) : base("Ошибка валидации")
            => Errors = errors;
    }
}