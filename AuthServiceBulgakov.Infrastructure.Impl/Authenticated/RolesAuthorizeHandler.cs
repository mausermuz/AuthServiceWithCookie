using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AuthServiceBulgakov.Infrastructure.Impl.Authenticated
{
    public class RolesAuthorizeHandler : AuthorizationHandler<RolesRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
        {
            if(context.User == null || !context.User.Identity.IsAuthenticated)
                return Task.CompletedTask;

            if (CheckRequiredRoles(context.User, requirement))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool CheckRequiredRoles(ClaimsPrincipal principal, RolesRequirement requirement)
        {
            var claims = GetClaims(principal);
            return HasRoles(claims, requirement);
        }

        private bool HasRoles(Claim[] claims, RolesRequirement rolesRequirement)
        {
            return claims.Any(x => rolesRequirement.Role == x.Value);
        }

        private Claim[] GetClaims(ClaimsPrincipal principal)
        {
            if (principal == null || !principal.Claims.Any(x => x.Type == "Roles"))
                return [];

            return principal.Claims.Where(x => x.Type == "Roles").ToArray();
        }
    }
}
