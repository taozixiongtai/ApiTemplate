using ApiTemplate.ServiceRegister;

namespace ApiTemplate.ServiceRegister;

public enum ServiceLifetimeType
{
    Singleton,
    Scoped,
    Transient
}

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class RegisterServiceAttribute(ServiceLifetimeType lifetime) : Attribute
{
    public ServiceLifetimeType Lifetime { get; } = lifetime;
}
