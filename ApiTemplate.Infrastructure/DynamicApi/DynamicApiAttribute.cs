using System;

namespace ApiTemplate.Infrastructure.DynamicApi;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class DynamicApiAttribute(string? routePrefix = null) : Attribute
{
    public string? RoutePrefix { get; } = routePrefix;
}

[AttributeUsage(AttributeTargets.Method)]
public class ApiActionAttribute : Attribute
{
    public string HttpMethod { get; }
    public string Route { get; }

    public ApiActionAttribute(string httpMethod, string route = "")
    {
        HttpMethod = httpMethod;  // "GET" "POST" "PUT" "DELETE" "PATCH"
        Route = route;
    }
}