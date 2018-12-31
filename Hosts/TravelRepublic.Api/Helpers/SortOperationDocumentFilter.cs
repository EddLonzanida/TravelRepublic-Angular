using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace TravelRepublic.Api.Helpers
{
    public class SortOperationDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = swaggerDoc.Paths.OrderBy(GetOrderKey)
                .ThenBy(e => e.Key)
                .ToList();
            swaggerDoc.Paths = paths.ToDictionary(e => e.Key, e => e.Value);
        }

        private static int GetOrderKey(KeyValuePair<string, PathItem> kvp)
        {
            if (kvp.Value.Delete != null) return 4;

            if (kvp.Value.Post != null) return 3;

            if (kvp.Value.Put != null) return 2;

            return kvp.Value.Get != null ? 1 : 9999;
        }
    }
}
