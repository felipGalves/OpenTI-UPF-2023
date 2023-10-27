using System.Linq.Expressions;
using AspNetCore.IQueryable.Extensions;
using Library.API.Entities;

namespace Library.API.Persistence.Contracts;

public interface IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> seletor, ICustomQueryable? filter = null);
Task<IEnumerable<TResult>> GetAllAsync<TResult, TProperty>(Expression<Func<TEntity, TResult>> seletor, Expression<Func<TEntity, TProperty>> include, ICustomQueryable? filter = null);    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task RemoveByIdAsync(Guid id);
    Task<TEntity> GetSingleAsync(Guid Id); 
    Task<TEntity> GetSingleAsync<TProperty>(Guid Id, Expression<Func<TEntity, TProperty>> include);
}
