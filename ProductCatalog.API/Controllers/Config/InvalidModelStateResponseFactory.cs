using Microsoft.AspNetCore.Mvc;
using ProductCatalog.API.Extensions;
using ProductCatalog.API.Resources;

namespace ProductCatalog.API.Controllers.Config
{
    public static class InvalidModelStateResponseFactory
    {
        public static IActionResult ProduceErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.GetErrorMessages();
            var response = new ErrorResource(messages: errors);

            return new BadRequestObjectResult(response);
        }
    }
}
