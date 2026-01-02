using System;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Swagger;

namespace Swashbuckle.Rebase.Extensions;

public static class SwaggerOptionsExtensions
{
    public static SwaggerOptions RemoveRoot(this SwaggerOptions options, string root)
    {
        return
            Rebase
            (
                options,
                originalPath =>
                {
                    var rebasedKey = originalPath;

                    if (originalPath.StartsWithSegments(root, StringComparison.InvariantCultureIgnoreCase, out var remaining))
                    {
                        rebasedKey = remaining;

                        if (string.IsNullOrWhiteSpace(rebasedKey))
                        {
                            rebasedKey = "/";
                        }
                    }

                    return rebasedKey;
                }
            );
    }

    public static SwaggerOptions Rebase(this SwaggerOptions options, Func<PathString, PathString> rebase)
    {
        options
            .PreSerializeFilters
            .Add
            (
                (swagger, _) =>
                {
                    var rebasedPaths = new OpenApiPaths();

                    foreach (var path in swagger.Paths)
                    {
                        rebasedPaths.Add(Uri.UnescapeDataString(rebase.Invoke(path.Key)), path.Value);
                    }

                    swagger.Paths = rebasedPaths;
                }
            );

        return options;
    }
}