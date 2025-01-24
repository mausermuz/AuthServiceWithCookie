namespace AuthServiceBulgakov.Domain.Constants
{
    public class Roles
    {
        public const string Admin = "admin";
        public const string User = "user";
    }

    public class DefaultRoles
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }

        public static DefaultRoles Admin = new()
        {
            Id = Guid.Parse("18c3a254-c61a-4e02-a8bd-c2f5297102ad"),
            RoleName = Roles.Admin,
        };

        public static DefaultRoles User = new()
        {
            Id = Guid.Parse("b2447ea1-ff84-4851-b67d-e3c9db76ca97"),
            RoleName = Roles.User,
        };

        public static DefaultRoles[] List = [Admin, User];
    }
}
