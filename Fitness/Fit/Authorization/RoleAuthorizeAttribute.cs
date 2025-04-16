using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Fit.Authorization
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string _role;
        public RoleAuthorizeAttribute(string role)
        {
            _role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var endpoint = context.HttpContext.GetEndpoint();
            var allowAnonymous = endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null;
            if (allowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            var user = context.HttpContext.User;
            var UserId = user.FindFirstValue("uid");

            if (UserId is null)
            {
                context.Result = new UnauthorizedObjectResult($"Access denied. You must be a Login.");
                return;
            }
            if(!user.IsInRole(_role))
            {
                //context.Result = new ForbidResult($"Access denied. You must be a {_role}.");
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = $"Access denied. You must be a {_role}."
                };
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
