namespace Synker.Domain.Entities.Core
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    [ExcludeFromCodeCoverage]
    public class EntityAudit
    {
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
