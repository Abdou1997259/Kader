using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kader_System.Domain.Customization.Attributes
{
    public class HandleValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = context.ModelState
                    .Where(entry => entry.Value.Errors.Any())
                    .ToDictionary(
                        entry => entry.Key,
                        entry => entry.Value.Errors.Select(error => error.ErrorMessage).ToArray()
                    );

                context.Result = new BadRequestObjectResult(new
                {
                    errors = validationErrors
                });
            }
        }
    }
}
