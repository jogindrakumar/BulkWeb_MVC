using BulkWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {
                
        }
        public DbSet<Category> Categories { get; set; }
    }
}
