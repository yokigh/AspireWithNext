using Microsoft.EntityFrameworkCore;
using MyAspireApp.WebApi.Models;

namespace MyAspireApp.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
    }
}
