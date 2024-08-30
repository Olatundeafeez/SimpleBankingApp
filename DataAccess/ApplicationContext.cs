using Microsoft.EntityFrameworkCore;
using simpleBankingAppAPI.Model;

namespace simpleBankingAppAPI.DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
       
        {
         
        }
        public DbSet<User> Users { get; set; }

    }
}
