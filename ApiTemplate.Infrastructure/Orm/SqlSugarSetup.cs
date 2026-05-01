using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace ApiTemplate.Infrastructure.Orm;

public static class SqlSugarSetup
{
    public static IServiceCollection AddSqlSugar(this IServiceCollection services, IConfiguration configuration)
    {
        var config = new ConnectionConfig();
        configuration.GetSection("SqlSugar").Bind(config);

        if (string.IsNullOrEmpty(config.ConnectionString))
        {
            throw new ArgumentNullException(nameof(config.ConnectionString), "未找到SqlSugar数据库连接配置");
        }

        var sqlSugar = new SqlSugarScope(
            config,
            db =>
            {
                // 可以添加AOP配置，如打印SQL等
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql); // 开发环境可以打印SQL
                };
            }
        );

        services.AddSingleton<ISqlSugarClient>(sqlSugar);
        return services;
    }
}