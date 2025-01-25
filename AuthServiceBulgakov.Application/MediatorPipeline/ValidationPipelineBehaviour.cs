using AuthServiceBulgakov.Domain.Exceptions;
using FluentValidation;
using MediatR;

namespace AuthServiceBulgakov.Application.MediatorPipeline
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
            => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .GroupBy(
                    x => x.PropertyName.Substring(x.PropertyName.IndexOf('.') + 1),
                    x => x.ErrorMessage, (propertyName, errorMessage) => new
                    {
                        Key = propertyName,
                        Values = errorMessage.Distinct().ToArray()
                    }
                ).ToDictionary(x => x.Key, y => y.Values);

            if (errorDictionary.Any())
                throw new ValidationApplicationException(errorDictionary);

            return await next();
        }
    }
}
