namespace Synker.Domain.Entities.Core
{
    public class EntityId<TId> : IEntityId<TId> where TId: struct
    {
        public TId Id { get; set; }
    }
}
