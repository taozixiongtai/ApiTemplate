using ApiTemplate.Application.Dto;
using ApiTemplate.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace ApiTemplate.Application.Mapper;

[Mapper]
public partial class ArticleMapper
{
    // 单对象
    public partial ArticleDto ToDto(Article entity);

    // 集合（必须显式写）
    public partial List<ArticleDto> ToDtoList(List<Article> entities);

    // 反向
    [MapperIgnoreTarget(nameof(Article.Id))]
    [MapperIgnoreTarget(nameof(Article.CreatedAt))]
    [MapperIgnoreTarget(nameof(Article.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Article.Categories))]
    [MapperIgnoreSource(nameof(ArticleRequest.CategoryIds))]
    public partial Article ToEntity(ArticleRequest dto);

    // 更新（非常重要）
    [MapperIgnoreTarget(nameof(Article.Id))]
    [MapperIgnoreTarget(nameof(Article.CreatedAt))]
    [MapperIgnoreTarget(nameof(Article.UpdatedAt))]
    [MapperIgnoreTarget(nameof(Article.Categories))]
    [MapperIgnoreSource(nameof(ArticleRequest.CategoryIds))]
    public partial void UpdateEntity(ArticleRequest dto, Article entity);
}