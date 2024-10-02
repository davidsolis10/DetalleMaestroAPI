using DetalleMaestroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DetalleMaestroAPI.Controllers;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
       base.OnModelCreating(modelBuilder);
    }
}