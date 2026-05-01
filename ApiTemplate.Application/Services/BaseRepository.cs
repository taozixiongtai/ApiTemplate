using System.Linq.Expressions;
using ApiTemplate.Application.IServices;
using SqlSugar;

namespace ApiTemplate.Application.Services;

public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
{
    protected readonly ISqlSugarClient _db;
    protected readonly ISugarQueryable<T> _entityDb;

    public BaseRepository(ISqlSugarClient db)
    {
        _db = db;
        _entityDb = _db.Queryable<T>();
    }

    public virtual ISugarQueryable<T> Query()
    {
        return _db.Queryable<T>();
    }

    public virtual async Task<List<T>> GetListAsync()
    {
        return await _db.Queryable<T>().ToListAsync();
    }

    public virtual async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(dynamic id)
    {
        return await _db.Queryable<T>().InSingleAsync(id);
    }

    public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).FirstAsync();
    }

    public virtual async Task<bool> InsertAsync(T entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> InsertRangeAsync(List<T> entities)
    {
        return await _db.Insertable(entities).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> whereExpression)
    {
        return await _db.Updateable(entity).Where(whereExpression).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        return await _db.Deleteable(entity).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> DeleteByIdAsync(dynamic id)
    {
        return await _db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await _db.Deleteable<T>().Where(whereExpression).ExecuteCommandAsync() > 0;
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await _db.Queryable<T>().WhereIF(whereExpression != null, whereExpression).CountAsync();
    }
}