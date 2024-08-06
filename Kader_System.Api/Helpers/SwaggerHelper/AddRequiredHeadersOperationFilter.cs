using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kader_System.Api.Helpers.SwaggerHelper
{
      public class AddHeadersOperationFilter : IOperationFilter
  {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters ??= [];

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "client_id",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Default = new OpenApiString("Kader") // Set the default value here


                },
                Description = "Client ID Header",
               
            });
        }
    }
}
