using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rost.WebApi.SwaggerConfig
{
    public class AddMajorVersionToBasePath : IDocumentFilter
    {
        private readonly int _majorVersion;

        public AddMajorVersionToBasePath(int majorVersion)
        {
            _majorVersion = majorVersion;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"/{_majorVersion}" } };
        }
    }
}