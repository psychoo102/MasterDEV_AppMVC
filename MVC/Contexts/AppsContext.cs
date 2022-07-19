using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Contexts
{
public class AppsContext : DbContext
{
        public DbSet<App> Apps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=x;Database=x;Username=x;Password=x");
    }
}
