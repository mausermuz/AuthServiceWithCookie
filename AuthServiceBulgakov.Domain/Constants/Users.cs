namespace AuthServiceBulgakov.Domain.Constants
{
    public class DefaultUsers
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public Guid[] RoleIds { get; set; } = [];

        public static DefaultUsers Admin = new()
        {
            Id = Guid.Parse("10c7ae6a-8759-47e1-84a0-90c11b98de7b"),
            UserName = "admin",
            Email = "admin@gmail.com",
            Password = "admin",
            IsActive = true,
            RoleIds = [DefaultRoles.Admin.Id]
        };

        public static DefaultUsers[] List = [DefaultUsers.Admin];
    }
}
