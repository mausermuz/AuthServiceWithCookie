using AuthServiceBulgakov.Application.Dto;
using MediatR;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public record LoginCommand(string UserName, string Password) : IRequest<LoginResponse>;
}
