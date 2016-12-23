using BusinessLayer.Models;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class RMSDbContext : DbContext
    {
        public RMSDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Department> Department { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}