using Microsoft.EntityFrameworkCore;
using Strunchik.Model.Item;
using Strunchik.Model.User;

namespace Strunchik.ViewModel.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;
    public DbSet<ItemModel> Items { get; set; } = null!;

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
    }
}
