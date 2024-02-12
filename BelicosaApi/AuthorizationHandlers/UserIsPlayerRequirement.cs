using BelicosaApi.BusinessLogic;
using BelicosaApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BelicosaApi.AuthorizationHandlers
{
    public class UserIsPlayerAuthorizationHandler : AuthorizationHandler<UserIsPlayerRequirement, Player>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserIsPlayerRequirement requirement, Player resource)
        {
            if (resource.UserId == context.User.Identity.GetUserId())
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(new AuthorizationFailureReason(this, "Player does not belong to logged in user"));
            }

            return Task.CompletedTask;
        }
    }

    public class UserIsPlayerRequirement : IAuthorizationRequirement { }
}
