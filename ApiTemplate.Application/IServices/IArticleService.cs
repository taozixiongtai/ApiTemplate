using ApiTemplate.Application.Dto;
using ApiTemplate.Infrastructure.DynamicApi;
using ApiTemplate.Infrastructure.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiTemplate.Domain.Dto;

namespace ApiTemplate.Application.IServices;

/// <summary>
/// 文章服务接口
/// </summary>
[DynamicApi("api/articles")]
public interface IArticleService
{
    /// <summary>
    /// 获取文章分页列表
    /// </summary>
    /// <param name="request">查询请求参数</param>
    /// <returns>文章分页结果</returns>
    [ApiAction("", HttpMethodType.GET)]
    [AllowAnonymous]
    Task<PagedResult<ArticleDto>> GetArticlesAsync([FromQuery] ArticleQueryRequest request);

    /// <summary>
    /// 根据ID获取文章详情
    /// </summary>
    /// <param name="id">文章ID</param>
    /// <returns>文章详情</returns>
    [ApiAction("{id}", HttpMethodType.GET)]
    [AllowAnonymous]
    Task<ArticleDto> GetArticleByIdAsync(int id);

    /// <summary>
    /// 创建文章
    /// </summary>
    /// <param name="dto">文章创建请求对象</param>
    /// <returns>创建后的文章信息</returns>
    [ApiAction("", HttpMethodType.POST)]
    Task<ArticleDto> CreateArticleAsync([FromBody] ArticleRequest dto);

    /// <summary>
    /// 更新文章
    /// </summary>
    /// <param name="id">文章ID</param>
    /// <param name="dto">文章更新请求对象</param>
    [ApiAction("{id}", HttpMethodType.PUT)]
    Task UpdateArticleAsync(int id, [FromBody] ArticleRequest dto);

    /// <summary>
    /// 删除文章
    /// </summary>
    /// <param name="id">文章ID</param>
    [ApiAction("{id}", HttpMethodType.DELETE)]
    Task DeleteArticleAsync(int id);
}