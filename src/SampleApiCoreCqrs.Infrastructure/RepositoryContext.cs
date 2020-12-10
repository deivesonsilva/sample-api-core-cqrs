using Microsoft.EntityFrameworkCore;
using SampleApiCoreCqrs.Infrastructure.Entities;
using SampleApiCoreCqrs.Infrastructure.Mappings;

namespace SampleApiCoreCqrs.Infrastructure
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AccountMap());
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
