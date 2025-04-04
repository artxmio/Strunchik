﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Strunchik.ViewModel.ApplicationContext;

#nullable disable

namespace Strunchik.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20250303100418_m8")]
    partial class m8
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Strunchik.Model.Basket.BasketModel", b =>
                {
                    b.Property<int>("BasketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BasketId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Strunchik.Model.Item.ItemModel", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BasketId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("ItemId");

                    b.HasIndex("BasketId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Strunchik.Model.User.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Strunchik.Model.Basket.BasketModel", b =>
                {
                    b.HasOne("Strunchik.Model.User.UserModel", "User")
                        .WithOne("Basket")
                        .HasForeignKey("Strunchik.Model.Basket.BasketModel", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Strunchik.Model.Item.ItemModel", b =>
                {
                    b.HasOne("Strunchik.Model.Basket.BasketModel", "Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");
                });

            modelBuilder.Entity("Strunchik.Model.Basket.BasketModel", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Strunchik.Model.User.UserModel", b =>
                {
                    b.Navigation("Basket")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
