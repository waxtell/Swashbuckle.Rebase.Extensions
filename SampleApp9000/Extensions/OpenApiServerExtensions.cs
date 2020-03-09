using Microsoft.OpenApi.Models;

namespace SampleApp9000.Extensions
{
    public static class OpenApiServerExtensions
    {
        public static OpenApiServer WithVariable(this OpenApiServer server, string key, OpenApiServerVariable value)
        {
            server.Variables.Add(key, value);
            return server;
        }
    }
}
