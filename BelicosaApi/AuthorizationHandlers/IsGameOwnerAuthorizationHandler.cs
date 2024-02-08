//using BelicosaApi.Models;
//using Microsoft.AspNet.Identity;
//using Microsoft.AspNetCore.Authorization;

//namespace BelicosaApi.AuthorizationHandlers
//{
//    public class IsGameOwnerAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, BelicosaGame>
//    {
//        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameAuthorRequirement requirement, BelicosaGame resource)
//        {
//            if (context.User.Identity.GetUserId() == resource.Owner.Id)
//            {

//            }
//        }
//    }

//    public class SameAuthorRequirement : IAuthorizationRequirement { }
//}
