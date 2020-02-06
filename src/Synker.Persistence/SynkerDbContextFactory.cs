namespace Synker.Persistence
{
    using Microsoft.EntityFrameworkCore;
    using Synker.Persistence.Infrastructure;

    public class SynkerDbContextFactory : DesignTimeDbContextFactoryBase<SynkerDbContext>
    {
        protected override SynkerDbContext CreateNewInstance(DbContextOptions<SynkerDbContext> options)
        {
            return new SynkerDbContext(options);
        }
    }
}
