using iapex.Models;
using Microsoft.EntityFrameworkCore;
namespace iapex.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Hospital> Hospital { get; set; }
        public DbSet<Institution> Institution { get; set; }
    }
}
