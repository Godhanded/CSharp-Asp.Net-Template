using CSharp_Asp.Net_Template.Domain.Entities;
using System.Linq.Expressions;

namespace CSharp_Asp.Net_Template.Infrastructure.Repository.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<T?> GetAsync(Guid id);

        Task<T?> GetBySpecAsync(Expression<Func<T,bool>> predicate, params Expression<Func<T,object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllBySpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> Exists(Guid id);

        IQueryable<T> GetQueryableBySpec(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        Task UpdateAsync(T entity);

        Task<T> DeleteAsync(T entity);

        Task DeleteRangeAsync(IEnumerable<T> entities);

        Task<int> CountAsync();

        Task<int> CountBySpecAsync(Expression<Func<T, bool>> predicate);

        Task SaveChangesAsync();
    }
}
