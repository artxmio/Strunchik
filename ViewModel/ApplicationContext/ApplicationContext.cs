using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Basket;
using Strunchik.Model.Item;
using Strunchik.Model.User;

namespace Strunchik.ViewModel.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<ItemModel> Items { get; set; } = null!;
    public DbSet<BasketModel> Baskets { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string sourcePathDB = "database.db";
        optionsBuilder.UseSqlite($"Data Source={sourcePathDB}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemModel>()
            .Property(i => i.Type)
            .HasConversion<int>();

        modelBuilder.Entity<UserModel>()
            .HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<BasketModel>(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BasketModel>()
            .HasMany(b => b.Items)
            .WithOne(i => i.Basket)
            .HasForeignKey(i => i.BasketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
