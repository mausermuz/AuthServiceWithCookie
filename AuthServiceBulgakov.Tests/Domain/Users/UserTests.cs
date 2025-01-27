using AuthServiceBulgakov.Application.Services;
using AuthServiceBulgakov.Domain.Entites;
using AuthServiceBulgakov.Infrastructure.Impl.Services;

namespace AuthServiceBulgakov.Tests.Domain.Users
{
    public class UserTests
    {
        [Fact]
        public void Success_Create_User()
        {
            //Arrange
            string userName = "johnDou";
            Guid userId = Guid.NewGuid();
            string password = "testPassword";
            string email = "test@gmail.com";
            IPasswordHasher passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.GenerateHash(password);

            //Act
            var userResult = User.Create(userId, userName, password, email);

            //Assert
            Assert.True(userResult.IsSuccess);
        }

        [Fact]
        public void Create_User_Failure_By_Empty_UserName()
        {
            //Arrange
            string userName = "";
            Guid userId = Guid.NewGuid();
            string password = "testPassword";
            string email = "test@gmail.com";
            IPasswordHasher passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.GenerateHash(password);

            //Act
            var userResult = User.Create(userId, userName, password, email);

            //Assert
            Assert.True(userResult.IsFailure);
            Assert.Equal("UserName не может быть пустым", userResult.Error);
        }

        [Fact]
        public void Create_User_Failure_By_Empty_Password()
        {
            //Arrange
            string userName = "testUser";
            Guid userId = Guid.NewGuid();
            string password = "";
            string email = "test@gmail.com";
            IPasswordHasher passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.GenerateHash(password);

            //Act
            var userResult = User.Create(userId, userName, password, email);

            //Assert
            Assert.True(userResult.IsFailure);
            Assert.Equal("Пароль не может быть пустым", userResult.Error);
        }

        [Fact]
        public void Create_User_Failure_By_Incrorrect_Email()
        {
            //Arrange
            string userName = "testUser";
            Guid userId = Guid.NewGuid();
            string password = "testPassword";
            string email = "test.com";
            IPasswordHasher passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.GenerateHash(password);

            //Act
            var userResult = User.Create(userId, userName, password, email);

            //Assert
            Assert.True(userResult.IsFailure);
            Assert.Equal("Введенная строка не является электронной почтой", userResult.Error);
        }

        [Fact]
        public void Create_User_Failure_By_Empty_Email()
        {
            //Arrange
            string userName = "testUser";
            Guid userId = Guid.NewGuid();
            string password = "testPassword";
            string email = "";
            IPasswordHasher passwordHasher = new PasswordHasher();
            string passwordHash = passwordHasher.GenerateHash(password);

            //Act
            var userResult = User.Create(userId, userName, password, email);

            //Assert
            Assert.True(userResult.IsFailure);
            Assert.Equal("Пустой email", userResult.Error);
        }
    }
}
