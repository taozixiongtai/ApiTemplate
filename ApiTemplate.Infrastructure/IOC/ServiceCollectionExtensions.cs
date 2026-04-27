using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApiTemplate.Infrastructure.IOC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoRegisteredServices(this IServiceCollection services, Assembly[]? assemblies = null)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            var basePath = AppContext.BaseDirectory;
            // 动态获取入口程序集（通常是主项目）名称的第一部分作为前缀，例如 "ApiTemplate"
            var entryAssemblyName = Assembly.GetEntryAssembly()?.GetName().Name;
            var prefix = entryAssemblyName?.Split('.')[0] ?? "*";
            
            var dllFiles = Directory.GetFiles(basePath, $"{prefix}*.dll");
            assemblies = [.. dllFiles.Select(Assembly.LoadFrom)];
        }

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
