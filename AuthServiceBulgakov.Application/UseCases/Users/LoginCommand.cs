using AuthServiceBulgakov.Application.Dto;
using MediatR;

namespace AuthServiceBulgakov.Application.UseCases.Users
{
    public record LoginCommand(string UserName, string Password) : IRequest<LoginResponse>;
}
