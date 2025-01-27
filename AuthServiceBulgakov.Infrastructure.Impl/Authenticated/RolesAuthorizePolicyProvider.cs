using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AuthServiceBulgakov.Infrastructure.Impl.Authenticated
{
    public class RolesAuthorizePolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public RolesAuthorizePolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public async override Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if (policy is not null)
                return policy;

            return new AuthorizationPolicyBuilder()
                .AddRequirements(new RolesRequirement(policyName))
                .Build();
        }
    }
}
