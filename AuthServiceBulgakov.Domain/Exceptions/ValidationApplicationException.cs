namespace AuthServiceBulgakov.Domain.Exceptions
{
    public class ValidationApplicationException : Exception
    {
        public ValidationApplicationException(string message) : base(message) { }
    }
}
