using ApiTemplate.Application.Dto;
using ApiTemplate.Infrastructure.DynamicApi;
using ApiTemplate.Infrastructure.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ApiTemplate.Application.IServices;

/// <summary>
/// 分类服务接口
/// </summary>
[DynamicApi("api/categories")]
public interface ICategoryService
{
    /// <summary>
    /// 获取所有分类
    /// </summary>
    /// <returns>分类列表</returns>
    [ApiAction("", HttpMethodType.GET)]
    [AllowAnonymous]
    Task<List<CategoryDto>> GetAllCategoriesAsync();

    /// <summary>
    /// 根据ID获取分类详情
    /// </summary>
    /// <param name="id">分类ID</param>
    /// <returns>分类详情</returns>
    [ApiAction("{id}", HttpMethodType.GET)]
    [AllowAnonymous]
    Task<CategoryDto> GetCategoryByIdAsync(int id);

    /// <summary>
    /// 创建分类
    /// </summary>
    /// <param name="dto">分类创建请求对象</param>
    /// <returns>创建后的分类信息</returns>
    [ApiAction("", HttpMethodType.POST)]
    Task<CategoryDto> CreateCategoryAsync([FromBody] CategoryRequest dto);

    /// <summary>
    /// 更新分类
    /// </summary>
    /// <param name="id">分类ID</param>
    /// <param name="dto">分类更新请求对象</param>
    [ApiAction("{id}", HttpMethodType.PUT)]
    Task UpdateCategoryAsync(int id, [FromBody] CategoryRequest dto);

    /// <summary>
    /// 删除分类
    /// </summary>
    /// <param name="id">分类ID</param>
    [ApiAction("{id}", HttpMethodType.DELETE)]
    Task DeleteCategoryAsync(int id);
}