using ApiTemplate.Domain.Models;
using SqlSugar;

namespace ApiTemplate.Init;

public static class DbInitSetup
{
    public static void InitializeDatabase(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var sqlSugar = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(DbInitSetup));

        // 1. 创建数据库 (如果不存在)
        // 注意：SQLite 会自动在指定路径创建文件，对于其他如 MySQL/SqlServer，这步是必须的
        sqlSugar.DbMaintenance.CreateDatabase();

        // 检查数据库中是否已经有表，如果有则直接跳过初始化
        var tables = sqlSugar.DbMaintenance.GetTableInfoList(false);
        if (tables != null && tables.Count > 0)
        {
            logger.LogInformation("表已初始化......");
            return;
        }

        // 2. 初始化表结构 (CodeFirst)
        // 可以通过命名空间批量扫描实体，也可以单独指定
        // 推荐方式：获取所有实体类所在的程序集并过滤出带有 SugarTable 特性的类
        var entityTypes = typeof(User).Assembly.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(SugarTable), false).Length != 0)
            .ToArray();

        if (entityTypes.Length > 0)
        {
            sqlSugar.CodeFirst.InitTables(entityTypes);

            // 3. 插入种子数据
            logger.LogInformation("正在插入种子数据...");
            
            using var tran = sqlSugar.AsTenant().UseTran();
            try
            {
                sqlSugar.Insertable(SeedData.GetSeedUsers()).ExecuteCommand();
                sqlSugar.Insertable(SeedData.GetSeedCategories()).ExecuteCommand();
                sqlSugar.Insertable(SeedData.GetSeedArticles()).ExecuteCommand();
                sqlSugar.Insertable(SeedData.GetSeedArticleCategoryRelations()).ExecuteCommand();
                
                tran.CommitTran();
                logger.LogInformation("种子数据插入成功！");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "插入种子数据失败");
                throw;
            }
        }

        logger.LogInformation("✅ 数据库及表结构初始化成功。");
    }
}