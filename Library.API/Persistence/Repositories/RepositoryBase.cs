using System.Linq.Expressions;
using AspNetCore.IQueryable.Extensions;
using AspNetCore.IQueryable.Extensions.Filter;
using Library.API.Data;
using Library.API.Entities;
using Library.API.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Persistence.Repositories;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : BaseEntity
{
    private readonly LibraryContext _conn;
    private readonly DbSet<TEntity> _dbSet;

    public RepositoryBase(LibraryContext conn)
    {
        _conn = conn;

        _dbSet = _conn.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        entity.DateAdded = DateTime.Now;

        await _dbSet.AddAsync(entity);
        await _conn.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _conn.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        var query = _dbSet.AsQueryable();

        if (filter != null)
            query = query
                .Where(filter)
                .AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<TResult>> GetAllAsync<TResult>(Expression<Func<TEntity, TResult>> seletor, ICustomQueryable? filter = null)
    {
        var query = _dbSet.AsQueryable().Filter(filter).Select(seletor);

        return await query.ToListAsync();
    }

      public async Task<IEnumerable<TResult>> GetAllAsync<TResult, TProperty>(Expression<Func<TEntity, TResult>> seletor, Expression<Func<TEntity, TProperty>> include, ICustomQueryable? filter = null)
    {
        var query = _dbSet.Include(include).AsQueryable().Filter(filter).Select(seletor);

        return await query.ToListAsync();
    }

    public async Task<TEntity> GetSingleAsync(Guid Id)
    {
        var entity = await _dbSet.FindAsync(Id);

        if (entity == null)
            throw new Exception();

        return entity;
    }

    public async Task<TEntity> GetSingleAsync<TProperty>(Guid Id, Expression<Func<TEntity, TProperty>> include)
    {
        var entity = await _dbSet.Include(include).FirstOrDefaultAsync(x => x.Id == Id);

        if (entity == null)
            throw new Exception();

        return entity;
    }

    public async Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _conn.SaveChangesAsync();
    }

     public async Task RemoveByIdAsync(Guid id)
    {
        var entity = await GetSingleAsync(id);

        _dbSet.Remove(entity);
        await _conn.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.DateModified = DateTime.Now;

        _conn.Entry(entity).State = EntityState.Modified;
        await _conn.SaveChangesAsync();
    }
}
