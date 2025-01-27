using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Entites;
using AuthServiceBulgakov.Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class ChangeStatusUsersCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<ChangeStatusUsersCommand>
    {
        public async Task Handle(ChangeStatusUsersCommand request, CancellationToken cancellationToken)
        {
            var userSpecForSetActive = UserSpecification.ByIds(request.ChangeStatusUsersDtos.Where(x => x.IsActive == true).Select(x => x.UserId).ToArray())
                                       & UserSpecification.ByIsActive(false);
            var userSpecForSetDeActive = UserSpecification.ByIds(request.ChangeStatusUsersDtos.Where(x => x.IsActive == false).Select(x => x.UserId).ToArray())
                                         & UserSpecification.ByIsActive(true);

            var userForActive = await dbContext.Users.Where(userSpecForSetActive).ToListAsync(cancellationToken);
            var userForDeActive = await dbContext.Users.Where(userSpecForSetDeActive).ToListAsync(cancellationToken);

            SetActiveOrDeactiveUser(userForActive);
            SetActiveOrDeactiveUser(userForDeActive, false);

            await dbContext.SaveChangesAsync();
        }

        public void SetActiveOrDeactiveUser(IReadOnlyList<User> users, bool isActive = true)
        {
            foreach(var user in users)
            {
                if (isActive)
                    user.SetActive();
                else
                    user.SetDeActive();
            }
        }
    }
}
