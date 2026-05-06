using ApiTemplate.Application.Dto;
using ApiTemplate.Domain.Models;
using Mapster;

namespace ApiTemplate.Application.Mapper;

[Mapper]
public interface IApplicationMapper
{
    CategoryDto MapToDto(Category category);
    List<CategoryDto> MapToDto(List<Category> categories);
    
    ArticleDto MapToDto(Article article);
    List<ArticleDto> MapToDto(List<Article> articles);
}