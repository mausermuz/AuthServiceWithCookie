using AuthServiceBulgakov.Domain.Seedwork;
using AuthServiceBulgakov.Domain.ValueObjects;
using CSharpFunctionalExtensions;

namespace AuthServiceBulgakov.Domain.Entites
{
    public class User : BaseEntity
    {
        public string UserName { get; private set; }
        public Email Email { get; private set; }
        public bool IsActive { get; private set; }
        public string PasswordHash { get; private set; }

        public List<Role> Roles = [];

        public RefreshToken? RefreshToken { get; private set; }

        #region Конструктор
        protected User()
        {
            
        }
        protected User(Guid id, string userName, string passwordHash, Email email)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            IsActive = true;
        }
        #endregion

        #region Метод создания пользователя
        public static Result<User> Create(Guid id, string userName, string passwordHash, string email)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return Result.Failure<User>("UserName не может быть пустым");

            if (string.IsNullOrWhiteSpace(passwordHash))
                return Result.Failure<User>("Пароль не может быть пустым");

            var emailResult = Email.Create(email);
            if (emailResult.IsFailure)
                return Result.Failure<User>(emailResult.Error);

            return Result.Success<User>(new User(id, userName, passwordHash, emailResult.Value));
        }
        #endregion

        #region DDD методы
        public void SetActive()
        {
            IsActive = true;
        }

        public void SetDective()
        {
            IsActive = false;
        }
        #endregion
    }
}
