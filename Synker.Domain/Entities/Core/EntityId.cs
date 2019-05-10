using System.Diagnostics.CodeAnalysis;

namespace Synker.Domain.Entities.Core
{
    [ExcludeFromCodeCoverage]
    public class EntityId<TId> : IEntityId<TId> where TId: struct
    {
        public TId Id { get; set; }
    }
}
