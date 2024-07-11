using Microsoft.EntityFrameworkCore;

namespace AkbasTest.Models
{
    public class PersonnelContext : DbContext
    {
        public PersonnelContext(DbContextOptions<PersonnelContext> options) : base(options)
        {
        }

        public DbSet<Personnels> Personnels { get; set; }
    }
}
