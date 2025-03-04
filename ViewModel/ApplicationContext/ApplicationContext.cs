using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Basket;
using Strunchik.Model.CartItem;
using Strunchik.Model.Item;
using Strunchik.Model.User;

namespace Strunchik.ViewModel.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<ItemModel> Items { get; set; } = null!;
    public DbSet<BasketModel> Baskets { get; set; } = null!;
    public DbSet<CartItemModel> CartItems { get; set; } = null!;

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

        modelBuilder.Entity<BasketModel>()
                  .HasOne(b => b.User)
                  .WithMany(u => u.Baskets)
                  .HasForeignKey(b => b.UserId);

        // Связь между CartItem и Basket
        modelBuilder.Entity<CartItemModel>()
            .HasOne(ci => ci.Basket)
            .WithMany(b => b.CartItems)
            .HasForeignKey(ci => ci.BasketId);

        // Связь между CartItem и Item
        modelBuilder.Entity<CartItemModel>()
            .HasOne(ci => ci.Item)
            .WithMany(i => i.CartItems)
            .HasForeignKey(ci => ci.ItemId);
    }
}
