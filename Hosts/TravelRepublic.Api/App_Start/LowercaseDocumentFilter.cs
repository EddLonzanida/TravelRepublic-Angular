using System.Collections.Generic;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace TravelRepublic.ApiHost
{
    public class LowercaseDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            var paths = swaggerDoc.paths;

            //	generate the new keys
            var newPaths = new Dictionary<string, PathItem>();
            var removeKeys = new List<string>();

            foreach (var path in paths)
            {
                var newKey = path.Key.ToLower();

                if (newKey == path.Key) continue;

                removeKeys.Add(path.Key);
                newPaths.Add(newKey, path.Value);
            }

            //	add the new keys
            foreach (var path in newPaths)
            {
                swaggerDoc.paths.Add(path.Key, path.Value);
            }

            //	remove the old keys
            foreach (var key in removeKeys)
            {
                swaggerDoc.paths.Remove(key);
            }
        }
    }
}