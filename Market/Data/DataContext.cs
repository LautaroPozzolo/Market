using Microsoft.EntityFrameworkCore;
using Market.Model;

namespace Market.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {  
        }

        public DbSet<Product> Product { get; set; }
    }
}
