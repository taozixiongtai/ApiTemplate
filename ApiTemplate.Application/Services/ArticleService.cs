using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Domain.Dto;
using ApiTemplate.Domain.Models;
using ApiTemplate.Infrastructure.IOC;
using SqlSugar;
using Check = ApiTemplate.Infrastructure.Check.Check;

namespace ApiTemplate.Application.Services;

/// <summary>
/// 文章服务实现类
/// </summary>
[RegisterService(ServiceLifetimeType.Scoped)]
public class ArticleService(
    IBaseRepository<Article> articleRepository,
    IBaseRepository<ArticleCategoryRelation> relationRepository,
    ISqlSugarClient db) : IArticleService
{
    /// <summary>
    /// 获取文章分页列表
    /// </summary>
    /// <param name="request">查询请求参数</param>
    /// <returns>文章分页结果</returns>
    public async Task<PagedResult<ArticleDto>> GetArticlesAsync(ArticleQueryRequest request)
    {
        var query = articleRepository.Query().Includes(a => a.Categories);

        if (request.CategoryId.HasValue)
        {
            var relations = await relationRepository.GetListAsync(x => x.CategoryId == request.CategoryId.Value);
            var articleIds = relations.Select(x => x.ArticleId).ToList();

            if (articleIds.Count == 0)
            {
                return new PagedResult<ArticleDto> { Page = request.Page, PageSize = request.PageSize };
            }

            query = query.In(a => a.Id, articleIds);
        }

        if (!string.IsNullOrWhiteSpace(request.KeyWord))
        {
            query = query.Where(a => a.Title.Contains(request.KeyWord) || a.Content.Contains(request.KeyWord));
        }

        RefAsync<int> totalCount = 0;
        var list = await query
            .OrderByDescending(a => a.CreatedAt)
            .ToPageListAsync(request.Page, request.PageSize, totalCount);

        return new PagedResult<ArticleDto>
        {
            Items = list.Select(MapToDto).ToList(),
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

    /// <summary>
    /// 根据ID获取文章详情
    /// </summary>
    /// <param name="id">文章ID</param>
    /// <returns>文章详情</returns>
    public async Task<ArticleDto> GetArticleByIdAsync(int id)
    {
        var article = await articleRepository.Query()
            .Includes(a => a.Categories)
            .InSingleAsync(id);

        Check.NotNull(article, "文章不存在");

        return MapToDto(article);
    }

    /// <summary>
    /// 创建文章
    /// </summary>
    /// <param name="dto">文章创建请求对象</param>
    /// <returns>创建后的文章信息</returns>
    public async Task<ArticleDto> CreateArticleAsync(ArticleRequest dto)
    {
        Check.NotEmpty(dto.Title, "文章标题不能为空");
        Check.IsTrue(dto.Title.Length <= 200, "文章标题不能超过200个字符");
        Check.NotEmpty(dto.Content, "文章内容不能为空");

        var article = new Article
        {
            Title = dto.Title,
            Content = dto.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await db.Ado.BeginTranAsync();
        try
        {
            var id = await articleRepository.InsertReturnIdentityAsync(article);

            if (dto.CategoryIds != null && dto.CategoryIds.Any())
            {
                var relations = dto.CategoryIds.Select(cid => new ArticleCategoryRelation
                {
                    ArticleId = id,
                    CategoryId = cid
                }).ToList();
                await relationRepository.InsertRangeAsync(relations);
            }

            await db.Ado.CommitTranAsync();

            return await GetArticleByIdAsync(id);
        }
        catch (Exception)
        {
            await db.Ado.RollbackTranAsync();
            throw;
        }
    }

    /// <summary>
    /// 更新文章
    /// </summary>
    /// <param name="id">文章ID</param>
    /// <param name="dto">文章更新请求对象</param>
    public async Task UpdateArticleAsync(int id, ArticleRequest dto)
    {
        Check.IsTrue(id > 0, "无效的文章ID");
        Check.NotEmpty(dto.Title, "文章标题不能为空");
        Check.IsTrue(dto.Title.Length <= 200, "文章标题不能超过200个字符");
        Check.NotEmpty(dto.Content, "文章内容不能为空");

        await db.Ado.BeginTranAsync();
        var article = await articleRepository.GetByIdAsync(id);
        Check.NotNull(article, "文章不存在或更新失败");

        article.Title = dto.Title;
        article.Content = dto.Content;
        article.UpdatedAt = DateTime.UtcNow;

        await articleRepository.UpdateAsync(article);

        await relationRepository.DeleteAsync(x => x.ArticleId == id);
        if (dto.CategoryIds != null && dto.CategoryIds.Count != 0)
        {
            var relations = dto.CategoryIds.Select(cid => new ArticleCategoryRelation
            {
                ArticleId = id,
                CategoryId = cid
            }).ToList();
            await relationRepository.InsertRangeAsync(relations);
        }

        await db.Ado.CommitTranAsync();
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <param name="id">文章ID</param>
    public async Task DeleteArticleAsync(int id)
    {
        Check.IsTrue(id > 0, "无效的文章ID");

        await db.Ado.BeginTranAsync();
        try
        {
            var result = await articleRepository.DeleteByIdAsync(id);
            Check.IsTrue(result, "文章不存在或删除失败");

            await relationRepository.DeleteAsync(x => x.ArticleId == id);

            await db.Ado.CommitTranAsync();
        }
        catch (Exception)
        {
            await db.Ado.RollbackTranAsync();
            throw;
        }
    }

    private static ArticleDto MapToDto(Article article)
    {
        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            Categories = article.Categories?.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList() ?? new List<CategoryDto>()
        };
    }
}