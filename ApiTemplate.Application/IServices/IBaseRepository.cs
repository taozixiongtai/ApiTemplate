using System.Linq.Expressions;
using SqlSugar;

namespace ApiTemplate.Application.IServices;

public interface IBaseRepository<T> where T : class, new()
{
    ISugarQueryable<T> Query();
    
    Task<List<T>> GetListAsync();
    
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);
    
    Task<T> GetByIdAsync(dynamic id);
    
    Task<T> GetFirstAsync(Expression<Func<T, bool>> whereExpression);
    
    Task<bool> InsertAsync(T entity);
    
    Task<bool> InsertRangeAsync(List<T> entities);
    
    Task<bool> UpdateAsync(T entity);
    
    Task<bool> UpdateAsync(T entity, Expression<Func<T, bool>> whereExpression);
    
    Task<bool> DeleteAsync(T entity);
    
    Task<bool> DeleteByIdAsync(dynamic id);
    
    Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);
    
    Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);
}