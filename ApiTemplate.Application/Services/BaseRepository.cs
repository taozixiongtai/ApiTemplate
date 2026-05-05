using System.Linq.Expressions;
using ApiTemplate.Application.IServices;
using ApiTemplate.Domain.Dto;
using SqlSugar;

namespace ApiTemplate.Application.Services;

/// <summary>
/// 仓储基础实现类
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public class BaseRepository<T>(ISqlSugarClient db) : IBaseRepository<T> where T : class, new()
{
    /// <summary>
    /// 获取查询对象
    /// </summary>
    /// <returns>查询对象</returns>
    public virtual ISugarQueryable<T> Query()
    {
        return db.Queryable<T>();
    }

    /// <summary>
    /// 获取所有列表
    /// </summary>
    /// <returns>实体列表</returns>
    public virtual async Task<List<T>> GetListAsync()
    {
        return await db.Queryable<T>().ToListAsync();
    }

    /// <summary>
    /// 根据条件获取列表
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>实体列表</returns>
    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
    }



    /// <summary>
    /// 根据主键获取实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体对象</returns>
    public virtual async Task<T> GetByIdAsync(dynamic id)
    {
        return await db.Queryable<T>().InSingleAsync(id);
    }

    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>实体对象</returns>
    public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).FirstAsync();
    }

    /// <summary>
    /// 插入实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> InsertAsync(T entity)
    {
        return await db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 批量插入实体
    /// </summary>
    /// <param name="entities">实体列表</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> InsertRangeAsync(List<T> entities)
    {
        return await db.Insertable(entities).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 插入实体并返回自增主键
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>自增主键值</returns>
    public virtual async Task<int> InsertReturnIdentityAsync(T entity)
    {
        return await db.Insertable(entity).ExecuteReturnIdentityAsync();
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> UpdateAsync(T entity)
    {
        return await db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据条件更新实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <param name="whereExpression">更新条件</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> whereExpression)
    {
        return await db.Updateable(entity).Where(whereExpression).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="entity">实体对象</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> DeleteAsync(T entity)
    {
        return await db.Deleteable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据主键删除实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> DeleteByIdAsync(dynamic id)
    {
        return await db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="whereExpression">删除条件</param>
    /// <returns>是否成功</returns>
    public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await db.Deleteable<T>().Where(whereExpression).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 根据条件统计数量
    /// </summary>
    /// <param name="whereExpression">查询条件</param>
    /// <returns>数量</returns>
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).CountAsync();
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="page">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <param name="whereExpression">查询条件（可选）</param>
    /// <param name="orderByExpression">排序条件（可选）</param>
    /// <param name="orderByType">排序方式（默认降序）</param>
    /// <returns>分页结果对象</returns>
    public virtual async Task<PagedResult<T>> GetPageListAsync(
        int page, 
        int pageSize, 
        Expression<Func<T, bool>>? whereExpression = null, 
        Expression<Func<T, object>>? orderByExpression = null, 
        OrderByType orderByType = OrderByType.Desc)
    {
        RefAsync<int> totalCount = 0;
        var list = await db.Queryable<T>()
            .WhereIF(whereExpression != null, whereExpression)
            .OrderByIF(orderByExpression != null, orderByExpression, orderByType)
            .ToPageListAsync(page, pageSize, totalCount);

        return new PagedResult<T>
        {
            Items = list,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }
}