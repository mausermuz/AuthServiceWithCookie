using FluentValidation;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage($"{nameof(LoginCommand.UserName)} не может быть пустым");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage($"{nameof(LoginCommand.Password)} не может быть пустым");
        }
    }
}
