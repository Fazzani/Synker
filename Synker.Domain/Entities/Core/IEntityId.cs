namespace Synker.Domain.Entities.Core
{
    public interface IEntityId<TId> where TId : struct
    {
        TId Id { get; set; }
    }
}