using AuthServiceBulgakov.Application.Dto;
using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Exceptions;
using AuthServiceBulgakov.Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceBulgakov.Application.UseCases.Users.Commands
{
    public class LoginCommandHandler(
        ApplicationDbContext dbContext,
        IJwtTokenService jwtTokenService,
        IPasswordHasher passwordHasher,
        IRefreshTokenService refreshTokenService) : IRequestHandler<LoginCommand, LoginResponse>
    {
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecification.ByUserName(request.UserName);

            var user = await dbContext.Users
                                      .Include(x => x.Roles)
                                      .Include(x => x.RefreshToken)
                                      .FirstOrDefaultAsync(spec, cancellationToken);

            if (user == null
                || !user.IsActive)
                throw new ValidationApplicationException("We could not log you in. Please check your username/password and try again");

            var accessToken = jwtTokenService.GenerateAccessToken(user);
            refreshTokenService.SaveRefreshToken(user);
            var refreshToken = refreshTokenService.GetStoredRefreshToken(user);

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = new LoginResponse(user.UserName!, accessToken, refreshToken);

            return response;
        }
    }
}
