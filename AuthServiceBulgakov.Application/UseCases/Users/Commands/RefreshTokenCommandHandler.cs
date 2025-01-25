using AuthServiceBulgakov.Application.Dto;
using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Exceptions;
using AuthServiceBulgakov.Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class RefreshTokenCommandHandler(
        ApplicationDbContext dbContext,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecification.ByRefreshToken(request.RefreshToken) & UserSpecification.ByUserName(request.UserName);
            var user = await dbContext.Users
                                      .Include(x => x.RefreshToken)
                                      .Include(x => x.Roles)
                                      .FirstOrDefaultAsync(spec, cancellationToken);

            if (user == null)
                throw new ValidationApplicationException("We could not log you in. Please check your username/password and try again");

            var accessToken = jwtTokenService.GenerateAccessToken(user);
            refreshTokenService.SaveRefreshToken(user);
            var refreshToken = refreshTokenService.GetStoredRefreshToken(user);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResponse(user.UserName!, accessToken, refreshToken, user.IsActive);
        }
    }
}
