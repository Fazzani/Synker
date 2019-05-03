namespace Synker.Domain.Entities.Core
{
    using System;

    public class EntityAudit
    {
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
