using System.Linq.Expressions;
using ApiTemplate.Domain.Dto;
using SqlSugar;

namespace ApiTemplate.Application.IServices;

/// <summary>
/// 仓储基础接口
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface IBaseRepository<T> where T : class, new()
{
    /// <summary>
    /// 获取查询对象
    /// </summary>
    /// <returns>查询对象</returns>
    ISugarQueryable<T> Query();
    
    /// <summary>
    /// 获取所有列表
    /// </summary>
    /// <returns>实体列表</returns>
    Task<List<T>> GetListAsync();
    
    /// <summary>
    /// 根据条件获取列表
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>实体列表</returns>
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);
    
    /// <summary>
    /// 根据主键获取实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体对象</returns>
    Task<T> GetByIdAsync(dynamic id);
    
    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>实体对象</returns>
    Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);
    
    /// <summary>
    /// 插入实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    Task<bool> InsertAsync(T entity);
    
    /// <summary>
    /// 批量插入实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>是否成功</returns>
    Task<bool> InsertRangeAsync(List<T> entities);
    
    /// <summary>
    /// 插入实体并返回自增主键
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>自增主键值</returns>
    Task<int> InsertReturnIdentityAsync(T entity);
    
    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(T entity);
    
    /// <summary>
    /// 根据条件更新实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="whereExpression">更新条件</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> whereExpression);
    
    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(T entity);
    
    /// <summary>
    /// 根据主键删除实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteByIdAsync(dynamic id);
    
    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="whereExpression">删除条件</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
    
    /// <summary>
    /// 根据条件统计数量
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>数量</returns>
    Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="whereExpression">查询条件（可选）</param>
    /// <param name="orderByExpression">排序条件（可选）</param>
    /// <param name="orderByType">排序方式（默认降序）</param>
    /// <returns>分页结果对象</returns>
    Task<PagedResult<T>> GetPageListAsync(
        int page, 
        int pageSize, 
        Expression<Func<T, bool>>? whereExpression = null, 
        Expression<Func<T, object>>? orderByExpression = null, 
        OrderByType orderByType = OrderByType.Desc);
}