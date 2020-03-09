# Swashbuckle.Rebase.Extensions
Rebase your API paths at Swashbuckle preserialization

![Build](https://github.com/waxtell/Swashbuckle.Rebase.Extensions/workflows/Build/badge.svg)
![Publish to nuget](https://github.com/waxtell/Swashbuckle.Rebase.Extensions/workflows/Publish%20to%20nuget/badge.svg?branch=master)

Configuration:
```csharp
            app.UseSwagger
            (
                options =>
                {
                    options.PreSerializeFilters.Add((swagger, httpReq) =>
                    {
                        if (!string.IsNullOrWhiteSpace(httpReq?.Host.Value))
                        {
                            swagger.Servers.Add
                            (
                                new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{{basePath}}" }
                                    .WithVariable("basePath", new OpenApiServerVariable { Default = "/test" })
                            );
                        }
                    });

                    options.RemoveRoot("/test");
                }
            );
```