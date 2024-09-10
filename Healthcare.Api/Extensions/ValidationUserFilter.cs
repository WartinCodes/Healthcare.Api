using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.ConstrainedExecution;

namespace Healthcare.Api.Extensions
{
    public class ValidationUserFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            var user = controller?.User;

            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userIdClaim = user.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;
            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (user.IsInRole(RoleEnum.Medico) || user.IsInRole(RoleEnum.Secretaria))
            {
                return;
            }

            if (context.ActionArguments.TryGetValue("userId", out var userId) && userId?.ToString() != userIdClaim)
            {
                context.Result = new ForbidResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
