namespace Synker.Domain.Entities.Core
{
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public class EntityIdAudit<TId> : EntityAudit, IEntityId<TId> where TId : struct
    {
        public TId Id { get; set; }
    }
}
