using Microsoft.EntityFrameworkCore;

namespace ClusterSalesManagementSystem.Models
{
    public class SalesDbContext :DbContext
    {
        public SalesDbContext(DbContextOptions option) : base(option)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Cluster> Clusters { get; set; }
        public DbSet<Sales> Sales { get; set; }
    }
}
