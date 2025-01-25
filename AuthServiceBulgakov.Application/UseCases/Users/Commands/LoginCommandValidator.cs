﻿using FluentValidation;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull().WithMessage("Логин не может быть пустым");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Пароль не может быть пустым");
        }
    }
}
