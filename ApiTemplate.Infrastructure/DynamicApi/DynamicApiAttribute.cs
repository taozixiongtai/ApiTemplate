using System;

namespace ApiTemplate.Infrastructure.DynamicApi;

[AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
public class DynamicApiAttribute(string? routePrefix = null) : Attribute
{
    public string? RoutePrefix { get; } = routePrefix;
}