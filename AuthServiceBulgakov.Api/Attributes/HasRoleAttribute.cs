using Microsoft.AspNetCore.Authorization;

namespace AuthServiceBulgakov.Api.Attributes
{
    public class HasRoleAttribute : AuthorizeAttribute
    {
        public string Role { get; }
        public HasRoleAttribute(string role) : base(policy: role)
        {
            Role = role;
        }
    }
}
