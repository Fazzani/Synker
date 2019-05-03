namespace Synker.Domain.Entities.Core
{
    public class EntityIdAudit<TId> : EntityAudit, IEntityId<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
