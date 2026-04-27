using System;

namespace ApiTemplate.Infrastructure.DynamicApi;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class DynamicApiAttribute : Attribute
{
    public string? RoutePrefix { get; }

    public DynamicApiAttribute(string? routePrefix = null)
    {
        RoutePrefix = routePrefix;
    }
}