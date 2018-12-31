using System.Linq;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TravelRepublic.Api.Helpers
{
    public class SwashbuckleSummaryOperationFilter: IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (!(context.ApiDescription.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)) return;

            var header = controllerActionDescriptor.ActionName.Split("_");

            if (header.Length > 1)
            {
                //get underscore separated header 
                var headerOnly = header.ToList().GetRange(1, header.Length - 1).ToArray();
                operation.Summary = string.Join("_", headerOnly);
            }
            else operation.Summary = header.First(); //if headers are not underscore separated

            if (operation.Parameters == null) return;

            var parameters = operation.Parameters.Select(p => p.Name);

            operation.Summary = $"{operation.Summary}({string.Join(", ", parameters)})";
        }
    }
}
