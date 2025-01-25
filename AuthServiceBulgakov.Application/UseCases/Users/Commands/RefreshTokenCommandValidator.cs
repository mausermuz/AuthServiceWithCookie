using FluentValidation;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Логин не может быть пустым");
        }
    }
}
