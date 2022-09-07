using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Implicat.Configuration;

public class ModelStateValidationActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var modelState = context.ModelState;

        if (!modelState.IsValid)
        {
            var errors = modelState.Values.Where(x => x.Errors != null)
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage);

            context.Result = new JsonResult(new { error = string.Join("; ", errors) })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            return;
        }

        await next();
    }
}