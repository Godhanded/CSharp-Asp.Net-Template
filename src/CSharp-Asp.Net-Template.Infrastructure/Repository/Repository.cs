using CSharp_Asp.Net_Template.Domain.Entities;
using CSharp_Asp.Net_Template.Infrastructure.Contexts;
using CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CSharp_Asp.Net_Template.Infrastructure.Repository
{
    public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : EntityBase
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().AnyAsync(predicate, cancellationToken);
        }

        public async Task<int> CountAsync()
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public async Task<int> CountBySpecAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().CountAsync(predicate);
        }

        public Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.FromResult(entity);
        }

        public Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _dbContext.Set<T>().AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _dbContext.Set<T>().AsNoTracking();

            foreach (var property in includeProperties)
                entities = entities.Include(property);

            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBySpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entities = _dbContext.Set<T>()
                                        .AsNoTracking()
                                        .Where(predicate);

            foreach (var property in includeProperties)
                entities = entities.Include(property);

            return await entities.ToListAsync();

        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _dbContext.Set<T>()
                                        .FindAsync(id);
        }

        public async Task<T?> GetBySpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entity = _dbContext.Set<T>()
                                                .AsNoTracking()
                                                .Where(predicate);

            foreach (var property in includeProperties)
                entity = entity.Include(property);

            return await entity.FirstOrDefaultAsync();
        }

        public IQueryable<T> GetQueryableBySpec(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task UpdateAsync(T entity)
        {
            var updatedEntity = _dbContext.Set<T>().Update(entity);
            return Task.FromResult(updatedEntity);
        }
    }
}
