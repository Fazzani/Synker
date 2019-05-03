namespace Synker.Application
{
    using Synker.Domain.Entities.Core;
    using Synker.Domain.Infrastructure;
    using System.Threading.Tasks;

    interface IServiceBase<TId, TEntity> 
        where TId: struct
        where TEntity : IEntityId<TId>, IAggregateRoot
    {
        Task<TEntity> GetAsync(TId id);
        Task<TEntity> DeleteAsync(TId id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TId id, TEntity entity);
    }
}
