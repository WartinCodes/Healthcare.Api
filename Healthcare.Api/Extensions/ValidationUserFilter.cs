using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Healthcare.Api.Extensions
{
    public class ValidationUserFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as ControllerBase;
            var userIdClaim = controller?.User?.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (userIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
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
