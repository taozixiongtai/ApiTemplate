using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Application.Mapper;
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
    private readonly ArticleMapper _mapper = new();

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
            Items = _mapper.ToDtoList(list),
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

        return _mapper.ToDto(article);
    }

    /// <summary>
    /// 创建文章
    /// </summary>
    /// <param name="dto">文章创建请求对象</param>
    /// <returns>创建后的文章信息</returns>
    public async Task<ArticleDto> CreateArticleAsync(ArticleRequest dto)
    {
        var article = _mapper.ToEntity(dto);
        article.CreatedAt = DateTime.UtcNow;
        article.UpdatedAt = DateTime.UtcNow;

        using var tran = db.AsTenant().UseTran();
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

        tran.CommitTran();

        return await GetArticleByIdAsync(id);
    }

    /// <summary>
    /// 更新文章
    /// </summary>
    /// <param name="id">文章ID</param>
    /// <param name="dto">文章更新请求对象</param>
    public async Task UpdateArticleAsync(int id, ArticleRequest dto)
    {

        using var tran = db.AsTenant().UseTran();
        var article = await articleRepository.GetByIdAsync(id);
        Check.NotNull(article, "文章不存在或更新失败");

        _mapper.UpdateEntity(dto, article);
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

        tran.CommitTran();
    }

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <param name="id">文章ID</param>
    public async Task DeleteArticleAsync(int id)
    {

        using var tran = db.AsTenant().UseTran();
        var result = await articleRepository.DeleteByIdAsync(id);
        Check.IsTrue(result, "文章不存在或删除失败");

        await relationRepository.DeleteAsync(x => x.ArticleId == id);

        tran.CommitTran();
    }
}