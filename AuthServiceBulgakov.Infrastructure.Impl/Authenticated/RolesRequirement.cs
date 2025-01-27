using Microsoft.AspNetCore.Authorization;

namespace AuthServiceBulgakov.Infrastructure.Impl.Authenticated
{
    public class RolesRequirement : IAuthorizationRequirement
    {
        public string Role { get; }
        public RolesRequirement(string role)
            => Role = role;
    }
}
