using Microsoft.EntityFrameworkCore;

namespace GraphQL_POC.Entities.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Account> Accounts { get; set; }
    }
}