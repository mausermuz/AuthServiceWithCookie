using AuthServiceBulgakov.Application.Dto.Users;
using MediatR;

namespace AuthServiceBulgakov.Application.UseCases.Users.Queries
{
    public record GetAllUsersQuery : IRequest<IReadOnlyList<UserListDto>>;
}
