using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthServiceBulgakov.Api.Filters
{
    public class HasRolesAttribute : ActionFilterAttribute
    {
        public string[] RequiredRoles { get; }
        public HasRolesAttribute(params string[] roles)
        {
            RequiredRoles = roles;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!CheckAuthorize(context))
                return;
            
            base.OnActionExecuted(context);
        }

        private bool CheckAuthorize(ActionExecutedContext context)
        {
            var user = context.HttpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return false;
            }

            if(!CheckRequiredRoles(RequiredRoles, user))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return false;
            }

            return true;
                
        }

        private bool CheckRequiredRoles(string[] roles, ClaimsPrincipal principal)
        {
            var claims = GetClaims(principal);
            return HasRoles(claims);
        }

        private bool HasRoles(Claim[] claims)
        {
            return claims.Any(x => RequiredRoles.Contains(x.Value));
        }

        private Claim[] GetClaims(ClaimsPrincipal principal)
        {
            if (principal == null || !principal.Claims.Any(x => x.Type == "Roles"))
                return [];

            return principal.Claims.Where(x => x.Type == "Roles").ToArray();
        }
    }
}
