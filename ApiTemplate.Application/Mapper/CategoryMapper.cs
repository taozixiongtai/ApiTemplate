using ApiTemplate.Application.Dto;
using ApiTemplate.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace ApiTemplate.Application.Mapper;

[Mapper]
public partial class CategoryMapper
{
    // 单对象
    public partial CategoryDto ToDto(Category entity);

    // 集合（必须显式写）
    public partial List<CategoryDto> ToDtoList(List<Category> entities);

    // 反向
    [MapperIgnoreTarget(nameof(Category.Id))]
    [MapperIgnoreTarget(nameof(Category.CreatedAt))]
    [MapperIgnoreTarget(nameof(Category.UpdatedAt))]
    public partial Category ToEntity(CategoryRequest dto);

    // 更新（非常重要）
    [MapperIgnoreTarget(nameof(Category.Id))]
    [MapperIgnoreTarget(nameof(Category.CreatedAt))]
    [MapperIgnoreTarget(nameof(Category.UpdatedAt))]
    public partial void UpdateEntity(CategoryRequest dto, Category entity);
}