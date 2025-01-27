using FluentValidation;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class ChangeStatusUsersCommandValidator : AbstractValidator<ChangeStatusUsersCommand>
    {
        public ChangeStatusUsersCommandValidator()
        {
            RuleFor(x => x.ChangeStatusUsersDtos).Custom((x, y) =>
            {
                if (x.Count() == 0)
                    y.AddFailure("Список пользователей не может быть пустым");
            });
        }
    }
}
