using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Contexts
{
public class AppsContext : DbContext
{
        public DbSet<App> Apps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=ec2-54-75-184-144.eu-west-1.compute.amazonaws.com;Database=dbaqmc80b9l532;Username=qrbientdzyfhup;Password=a6175c149104a75b9087951887a2c0b8fccfed2829ccf5c61b3d24ef98067bd1");
    }
}