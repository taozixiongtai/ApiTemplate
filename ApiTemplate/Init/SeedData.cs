using ApiTemplate.Domain.Models;

namespace ApiTemplate.Init;

public class SeedData
{
    public static List<User> GetSeedUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = 1,
                Username = "wjt",
                Password = "wjtwjt", // 实际项目中应当加密存储
                Nickname = "超级管理员",
                CreateTime = DateTime.UtcNow
            }
        };
    }

    public static List<Category> GetSeedCategories()
    {
        return new List<Category>
        {
            new Category { Id = 1, Name = "C# 编程", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 2, Name = "ASP.NET Core", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 3, Name = "前端开发", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        };
    }

    public static List<Article> GetSeedArticles()
    {
        return new List<Article>
        {
            new Article
            {
                Id = 1,
                Title = "C# 12 新特性解析",
                Content = "主构造函数、集合表达式等特性极大提高了开发效率...",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Article
            {
                Id = 2,
                Title = "ASP.NET Core Web API 最佳实践",
                Content = "全局异常处理、依赖注入、自动接口生成是现代化 API 必不可少的组件...",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };
    }

    public static List<ArticleCategoryRelation> GetSeedArticleCategoryRelations()
    {
        return new List<ArticleCategoryRelation>
        {
            new ArticleCategoryRelation { ArticleId = 1, CategoryId = 1 }, // C# 12 -> C# 编程
            new ArticleCategoryRelation { ArticleId = 2, CategoryId = 1 }, // ASP.NET Core -> C# 编程
            new ArticleCategoryRelation { ArticleId = 2, CategoryId = 2 }  // ASP.NET Core -> ASP.NET Core
        };
    }
}
