using System.Linq;
using System.Linq.Expressions;
using BN.CleanArchitecture.Core.Domain.Entities;
using BN.CleanArchitecture.Core.Repository;
using BN.CleanArchitecture.Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace BN.CleanArchitecture.Infrastructure.EfCore;

public class RepositoryBase<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>, IGridRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public RepositoryBase(TDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<TEntity> FindByIdAsync(TKey id)
    {
        return await _dbContext.Set<TEntity>().SingleOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<TEntity> FindOneAsync(ISpecification<TEntity> spec)
    {
        IQueryable<TEntity> specificationResult = GetQuery(_dbContext.Set<TEntity>(), spec);

        return await specificationResult.FirstOrDefaultAsync();
    }

    public async Task<List<TEntity>> FindAsync(ISpecification<TEntity> spec)
    {
        if (spec == null)
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        IQueryable<TEntity> specificationResult = GetQuery(_dbContext.Set<TEntity>(), spec);

        return await specificationResult.ToListAsync();
    }

    public async ValueTask<long> CountAsync(IGridSpecification<TEntity> spec)
    {
        spec.IsPagingEnabled = false;
        IQueryable<TEntity> specificationResult = GetQuery(_dbContext.Set<TEntity>(), spec);

        return await ValueTask.FromResult(specificationResult.LongCount());
    }

    public async Task<List<TEntity>> FindAsync(IGridSpecification<TEntity> spec)
    {
        IQueryable<TEntity> specificationResult = GetQuery(_dbContext.Set<TEntity>(), spec);

        return await specificationResult.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);

        await _dbContext.SaveChangesAsync();

        return entity;
    }

   public async Task RemoveAsync(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().AnyAsync(predicate);
    }

    private static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = inputQuery;

        if (specification.Criteria is not null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip - 1)
                .Take(specification.Take);
        }

        return query;
    }

    private static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        IGridSpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = inputQuery;

        if (specification.Criterias is not null && specification.Criterias.Count > 0)
        {
            Expression<Func<TEntity, bool>> expr = specification.Criterias.First();
            for (int i = 1; i < specification.Criterias.Count; i++)
            {
                expr = expr.And(specification.Criterias[i]);
            }

            query = query.Where(expr);
        }

        query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

        if (specification.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy is not null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip - 1)
                .Take(specification.Take);
        }

        return query;
    }
}