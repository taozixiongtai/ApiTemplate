using ApiTemplate.ServiceRegister;
using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoRegisteredServices(this IServiceCollection services, Assembly[]? assemblies = null)
    {
        assemblies ??= [Assembly.GetExecutingAssembly()];

        foreach (var type in assemblies.SelectMany(a => a.GetTypes()).Where(t => t.IsClass && !t.IsAbstract))
        {
            var attr = type.GetCustomAttribute<RegisterServiceAttribute>();
            if (attr == null) continue;

            var interfaces = type.GetInterfaces();
            if (interfaces.Length == 0) continue;

            foreach (var iface in interfaces)
            {
                switch (attr.Lifetime)
                {
                    case ServiceLifetimeType.Singleton:
                        services.AddSingleton(iface, type);
                        break;
                    case ServiceLifetimeType.Scoped:
                        services.AddScoped(iface, type);
                        break;
                    case ServiceLifetimeType.Transient:
                        services.AddTransient(iface, type);
                        break;
                }
            }
        }

        return services;
    }
}
