﻿using Microsoft.EntityFrameworkCore;
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
    public DbSet<ItemsType> InstrumentTypes { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        const string sourcePathDB = "database.db";
        optionsBuilder.UseSqlite($"Data Source={sourcePathDB}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .HasOne(u => u.Basket)
            .WithOne(b => b.User)
            .HasForeignKey<BasketModel>(b => b.UserId);

        modelBuilder.Entity<CartItemModel>()
            .HasOne(ci => ci.Basket)
            .WithMany(b => b.CartItems)
            .HasForeignKey(ci => ci.BasketId);

        modelBuilder.Entity<CartItemModel>()
            .HasOne(ci => ci.Item)
            .WithMany(i => i.CartItems)
            .HasForeignKey(ci => ci.ItemId);

        modelBuilder.Entity<ItemModel>()
            .HasOne(mi => mi.ItemType)
            .WithMany()
            .HasForeignKey(mi => mi.TypeId);
    }
}
