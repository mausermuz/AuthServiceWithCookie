using AuthServiceBulgakov.Application.Dto;
using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceBulgakov.Application.UseCases.Users
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
                throw new Exception("Юзер не найден");

            var accessToken = jwtTokenService.GenerateAccessToken(user);
            refreshTokenService.SaveRefreshToken(user);
            var refreshToken = refreshTokenService.GetStoredRefreshToken(user);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResponse(user.UserName!, accessToken, refreshToken);
        }
    }
}
