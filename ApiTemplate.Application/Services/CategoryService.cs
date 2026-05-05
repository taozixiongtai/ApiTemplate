using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Domain.Models;
using ApiTemplate.Infrastructure.Check;
using ApiTemplate.Infrastructure.IOC;
using SqlSugar;
using Check = ApiTemplate.Infrastructure.Check.Check;

namespace ApiTemplate.Application.Services;

/// <summary>
/// 分类服务实现类
/// </summary>
[RegisterService(ServiceLifetimeType.Scoped)]
public class CategoryService(
    IBaseRepository<Category> categoryRepository,
    IBaseRepository<ArticleCategoryRelation> relationRepository,
    ISqlSugarClient db) : ICategoryService
{
    /// <summary>
    /// 获取所有分类
    /// </summary>
    /// <returns>分类列表</returns>
    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetListAsync();
        return categories.Select(MapToDto).ToList();
    }

    /// <summary>
    /// 根据ID获取分类详情
    /// </summary>
    /// <param name="id">分类ID</param>
    /// <returns>分类详情</returns>
    public async Task<CategoryDto> GetCategoryByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        Check.NotNull(category, "分类不存在");
        return MapToDto(category);
    }

    /// <summary>
    /// 创建分类
    /// </summary>
    /// <param name="dto">分类创建请求对象</param>
    /// <returns>创建后的分类信息</returns>
    public async Task<CategoryDto> CreateCategoryAsync(CategoryRequest dto)
    {
        Check.NotEmpty(dto.Name, "分类名称不能为空");
        Check.IsTrue(dto.Name.Length <= 200, "分类名称不能超过200个字符");

        var category = new Category
        {
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        var id = await categoryRepository.InsertReturnIdentityAsync(category);
        return await GetCategoryByIdAsync(id);
    }

    /// <summary>
    /// 更新分类
    /// </summary>
    /// <param name="id">分类ID</param>
    /// <param name="dto">分类更新请求对象</param>
    public async Task UpdateCategoryAsync(int id, CategoryRequest dto)
    {
        Check.IsTrue(id > 0, "无效的分类ID");
        Check.NotEmpty(dto.Name, "分类名称不能为空");
        Check.IsTrue(dto.Name.Length <= 200, "分类名称不能超过200个字符");

        var category = await categoryRepository.GetByIdAsync(id);
        Check.NotNull(category, "分类不存在或更新失败");

        category.Name = dto.Name;
        category.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.UpdateAsync(category);
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <param name="id">分类ID</param>
    public async Task DeleteCategoryAsync(int id)
    {
        Check.IsTrue(id > 0, "无效的分类ID");

        await db.Ado.BeginTranAsync();
        try
        {
            var result = await categoryRepository.DeleteByIdAsync(id);
            Check.IsTrue(result, "分类不存在或删除失败");

            await relationRepository.DeleteAsync(x => x.CategoryId == id);
            
            await db.Ado.CommitTranAsync();
        }
        catch (Exception)
        {
            await db.Ado.RollbackTranAsync();
            throw;
        }
    }

    private static CategoryDto MapToDto(Category category)
    {
        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}