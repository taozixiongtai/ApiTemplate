namespace ApiTemplate.Infrastructure.IOC;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RegisterServiceAttribute(ServiceLifetimeType lifetime) : Attribute
{
    public ServiceLifetimeType Lifetime { get; } = lifetime;
}
