using ApiTemplate.Application.Dto;
using ApiTemplate.Application.IServices;
using ApiTemplate.Application.Mapper;
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
    private readonly CategoryMapper _mapper = new();

    /// <summary>
    /// 获取所有分类
    /// </summary>
    /// <returns>分类列表</returns>
    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetListAsync();
        return _mapper.ToDtoList(categories);
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
        return _mapper.ToDto(category);
    }

    /// <summary>
    /// 创建分类
    /// </summary>
    /// <param name="dto">分类创建请求对象</param>
    /// <returns>创建后的分类信息</returns>
    public async Task<CategoryDto> CreateCategoryAsync(CategoryRequest dto)
    {
        var category = _mapper.ToEntity(dto);
        category.CreatedAt = DateTime.UtcNow;
        category.UpdatedAt = DateTime.UtcNow;
        
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

        var category = await categoryRepository.GetByIdAsync(id);
        Check.NotNull(category, "分类不存在或更新失败");

        _mapper.UpdateEntity(dto, category);
        category.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.UpdateAsync(category);
    }

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <param name="id">分类ID</param>
    public async Task DeleteCategoryAsync(int id)
    {

        using var tran = db.AsTenant().UseTran();
        var result = await categoryRepository.DeleteByIdAsync(id);
        Check.IsTrue(result, "分类不存在或删除失败");

        await relationRepository.DeleteAsync(x => x.CategoryId == id);
            
        tran.CommitTran();
    }
}