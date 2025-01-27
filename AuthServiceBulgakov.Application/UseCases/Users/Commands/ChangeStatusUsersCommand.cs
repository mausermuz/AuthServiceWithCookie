using AuthServiceBulgakov.Application.Dto.Users;
using MediatR;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public record ChangeStatusUsersCommand(ChangeStatusUsersDto[] ChangeStatusUsersDtos) : IRequest;
}
