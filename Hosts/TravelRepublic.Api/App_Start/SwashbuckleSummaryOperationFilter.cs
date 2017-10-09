using System.Linq;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace TravelRepublic.ApiHost
{
    public class SwashbuckleSummaryOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var header = operation.operationId.Split('_');
            if (header.Length > 1)
            {
                //get underscore separated header 
                var headerOnly = header.ToList().GetRange(1, header.Length - 1).ToArray();
                operation.summary = string.Join("_", headerOnly);
            }
            else operation.summary = operation.operationId; //if headers are not underscore separated

            if (operation.parameters == null) return;

            var parameters = operation.parameters.Select(p => p.name);
            operation.summary = $"{operation.summary}({string.Join(", ", parameters)})";
        }
    }
}