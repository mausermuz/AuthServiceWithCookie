using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.DataAccess.MSSQL;
using AuthServiceBulgakov.Domain.Constants;
using AuthServiceBulgakov.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuthServiceBulgakov.Api.Helpers
{
    public class DbIniailize(
        ApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        ILogger<DbIniailize> logger)
    {

        public async Task InitializeDatabase()
        {
            logger.LogInformation("Старт процесса миграции и ининциализации базы данных...");

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
                {
                    using var transaction = await dbContext.Database.BeginTransactionAsync();

                    try
                    {
                        var pendingMigration = await dbContext.Database.GetPendingMigrationsAsync();
                        if (pendingMigration.Any())
                        {
                            logger.LogInformation("Применение миграций для базы данных...");
                            await dbContext.Database.MigrateAsync();
                            logger.LogInformation("Мигарции применились");
                        }

                        await InitializeDefaultRoles();
                        await InitializeDefaultUsers();

                        await transaction.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        logger.LogError(e, "Ошибка при инициализации базы данных: {0}", e.Message);
                        await transaction.RollbackAsync();
                        throw;
                    }
                });

           

            
        }

        private async Task InitializeDefaultRoles()
        {
            logger.LogInformation("Старт процесса ининциализации базы данных дефолтными ролями...");

            foreach (var role in DefaultRoles.List)
            {
                var savedRole = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == role.Id);
                if (savedRole == null)
                {
                    savedRole = new Role(role.Id, role.RoleName);
                    await dbContext.Roles.AddAsync(savedRole);
                }
            }

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Окончание инициализации базы данных дефолтными ролями");
        }

        private async Task InitializeDefaultUsers()
        {
            logger.LogInformation("Старт процесса ининциализации базы данных дефолтными пользователями...");

            foreach(var user in DefaultUsers.List)
            {
                var savedUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (savedUser == null)
                {
                    var hashPassword = passwordHasher.GenerateHash(user.Password);
                    var userResult = User.Create(user.Id, user.UserName, hashPassword, user.Email);
                    if (!userResult.IsSuccess)
                        throw new Exception($"Ошибка при записи дефолтного пользователя в базу данных с userName {user.UserName}: {userResult.Error}");

                    var savedUserRoles = await dbContext.Roles.Where(x => user.RoleIds.Contains(x.Id)).ToListAsync();
                    if(savedUserRoles.Count == 0)
                        throw new Exception($"Не найдены роли при записи дефолтного пользователя в базу данных с userName {user.UserName}. Id ролей {string.Join("," ,user.RoleIds.Select(x => x.ToString()))}");

                    savedUser = userResult.Value;
                    savedUser.Roles.AddRange(savedUserRoles);
                    await dbContext.Users.AddAsync(savedUser);
                }
            }

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Окончание инициализации базы данных дефолтными пользователями");
        }
    }
}
