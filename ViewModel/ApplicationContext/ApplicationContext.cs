using Microsoft.EntityFrameworkCore;
using Strunchik.Model.User;

namespace Strunchik.ViewModel.ApplicationContext;

public class ApplicationContext : DbContext
{
    public DbSet<UserModel> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string sourcePathDB = "database.db";
        optionsBuilder.UseSqlite($"Data Source={sourcePathDB}");
    }
}
