namespace Synker.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Synker.Persistence.Infrastructure;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SynkerDbContextFactory : DesignTimeDbContextFactoryBase<SynkerDbContext>
    {
        protected override SynkerDbContext CreateNewInstance(DbContextOptions<SynkerDbContext> options)
        {
            return new SynkerDbContext(options);
        }
    }
}
