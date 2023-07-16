using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using SImpl.Http.Ping.Module;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SImpl.Http.Ping.AspNetCore;

public class PingControllerSwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (PingModule.Enabled)
        {
            swaggerDoc.Paths.Add($"{PingModule.ModuleConfig.RoutePrefix}/ping/ping", new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>()
                {
                    [OperationType.Get] = new()
                    {
                        Responses = new OpenApiResponses
                        {
                            {
                                "200", new OpenApiResponse
                                {
                                    Content = new Dictionary<string, OpenApiMediaType>
                                    {
                                        ["text/plain"] = new OpenApiMediaType
                                        {
                                            Example = new OpenApiString("03/24/2021 16:43:41 pong"),
                                            Schema = new OpenApiSchema
                                            {
                                                Type = "string"
                                            }
                                        }
                                    },
                                    Description = "Success",
                                }
                            },
                            {
                                "400", new OpenApiResponse
                                {
                                    Description = "Not Found",
                                }
                            }
                        },
                        Summary = "Ping to test whether the microservice is reachable",
                        Tags = new List<OpenApiTag>
                        {
                            new()
                            {
                                Name = PingModule.ModuleConfig.ControllerArea,
                            }
                        }
                    }
                },
            });
        }
    }
}