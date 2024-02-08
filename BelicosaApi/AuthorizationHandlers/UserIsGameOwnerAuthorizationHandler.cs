using BelicosaApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BelicosaApi.AuthorizationHandlers
{
    public class UserIsGameOwnerAuthorizationHandler : AuthorizationHandler<UserIsGameOwnerRequirement, BelicosaGame>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsGameOwnerRequirement requirement, BelicosaGame resource)
        {
            if (context.User.Identity.GetUserId() == resource.OwnerId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class UserIsGameOwnerRequirement : IAuthorizationRequirement { }
}
