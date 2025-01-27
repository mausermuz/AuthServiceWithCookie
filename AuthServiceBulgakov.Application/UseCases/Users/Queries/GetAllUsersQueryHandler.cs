using AuthServiceBulgakov.Application.Dto.Users;
using AuthServiceBulgakov.DataAccess.MSSQL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using AuthServiceBulgakov.Application.Extensions;

namespace AuthServiceBulgakov.Application.UseCases.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserListDto>>
    {
        
        private readonly ConfigurationOptions _option;
        private readonly ApplicationDbContext _dbContext;

        public const string USERS_KEY = "get_users_key";

        public GetAllUsersQueryHandler(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;

            _option = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = 
                {
                    configuration.GetSection("Redis").Value!
                }
            };
        }

        public async Task<IReadOnlyList<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var listDto = new List<UserListDto>();

            await using var redis = await ConnectionMultiplexer.ConnectAsync(_option);
            var db = redis.GetDatabase();
            var resultString = await db.GetAsync<IReadOnlyList<UserListDto>>(USERS_KEY);

            if(resultString == null)
            {
                listDto = await _dbContext.Users.AsNoTracking()
                                                .Select(x => new UserListDto
                                                {
                                                    Id = x.Id,
                                                    UserName = x.UserName,
                                                    IsActive = x.IsActive,
                                                }).ToListAsync(cancellationToken);

                await db.SetAsync<IReadOnlyList<UserListDto>>(USERS_KEY, listDto, TimeSpan.FromMinutes(10));

                return listDto;
            }

            return resultString;
        }
    }
}
