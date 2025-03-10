using CafeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeBackend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<TableReservation> TableReservations { get; set; }
    public DbSet<CoffeeType> CoffeeTypes { get; set; }
    public DbSet<CoffeeCustomization> CoffeeCustomizations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany()
            .HasForeignKey(o => o.UserId);

        modelBuilder.Entity<TableReservation>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId);
    }
}