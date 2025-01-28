using AuthServiceBulgakov.Application.Dto.Users;
using AuthServiceBulgakov.DataAccess.MSSQL;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using AuthServiceBulgakov.Application.Extensions;
using Dapper;
using AuthServiceBulgakov.Domain.Entites;

namespace AuthServiceBulgakov.Application.UseCases.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IReadOnlyList<UserListDto>>
    {
        
        private readonly ConfigurationOptions _option;
        private readonly ApplicationDbContext _dbContext;
        private readonly DapperContext _dapperContext;

        public const string USERS_KEY = "get_users_key";

        public GetAllUsersQueryHandler(
            ApplicationDbContext dbContext, 
            IConfiguration configuration,
            DapperContext dapperContext)
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

            _dapperContext = dapperContext;
        }

        public async Task<IReadOnlyList<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<UserListDto> listDto;

            await using var redis = await ConnectionMultiplexer.ConnectAsync(_option);
            var db = redis.GetDatabase();
            var resultString = await db.GetAsync<IReadOnlyList<UserListDto>>(USERS_KEY);

            if (resultString == null)
            {
                string query = "SELECT Id, UserName, IsActive FROM dbo.Users";

                using var connection = _dapperContext.CreateConnetion();
                listDto = await connection.QueryAsync<UserListDto>(query);

                await db.SetAsync<IReadOnlyList<UserListDto>>(USERS_KEY, listDto.ToList(), TimeSpan.FromMinutes(10));

                return listDto.ToList();
            }

            return resultString;
        }
    }
}
