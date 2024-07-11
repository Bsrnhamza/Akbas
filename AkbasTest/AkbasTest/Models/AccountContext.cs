using Microsoft.EntityFrameworkCore;

namespace AkbasTest.Models
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
    }
}
