using Microsoft.EntityFrameworkCore;
using ApiRestNet.Models;

namespace ApiRestNet.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ApiRestNet.Models.Task> Tasks { get; set; }
    }
}