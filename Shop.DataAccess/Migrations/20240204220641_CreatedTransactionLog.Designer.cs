﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.DataAccess.Data_Access;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240204220641_CreatedTransactionLog")]
    partial class CreatedTransactionLog
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shop.Core.Entities.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("Shop.Core.Entities.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Shop.Core.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CardHolderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Cvc")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.HasIndex("CardNumber", "Cvc")
                        .IsUnique()
                        .HasFilter("[CardNumber] IS NOT NULL");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("Shop.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Shop.Core.Entities.DeliveryAddress", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("DeliveryAddresses");
                });

            modelBuilder.Entity("Shop.Core.Entities.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("DiscountPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Shop.Core.Entities.InvoiceItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("Shop.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("QuantityAvailable")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("DiscountId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Core.Entities.TransactionLog", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CardId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("TransactionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CardId");

                    b.HasIndex("UserId");

                    b.ToTable("TransactionLogs");
                });

            modelBuilder.Entity("Shop.Core.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserName", "Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"), 1L, 1);

                    b.Property<decimal?>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("Shop.Core.Entities.Basket", b =>
                {
                    b.HasOne("Shop.Core.Entities.Product", "Product")
                        .WithMany("Baskets")
                        .HasForeignKey("ProductId");

                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("Baskets")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.Card", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("UserId");

                    b.HasOne("Shop.Core.Entities.Wallet", "Wallet")
                        .WithMany("Cards")
                        .HasForeignKey("WalletId");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Shop.Core.Entities.DeliveryAddress", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("DeliveryAddresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("Invoices")
                        .HasForeignKey("UserId");

                    b.HasOne("Shop.Core.Entities.Wallet", "Wallet")
                        .WithMany("Invoices")
                        .HasForeignKey("WalletId");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Shop.Core.Entities.InvoiceItem", b =>
                {
                    b.HasOne("Shop.Core.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("Shop.Core.Entities.Product", "Product")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("ProductId");

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Core.Entities.Product", b =>
                {
                    b.HasOne("Shop.Core.Entities.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId");

                    b.HasOne("Shop.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Shop.Core.Entities.Discount", "Discount")
                        .WithMany("Products")
                        .HasForeignKey("DiscountId");

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Shop.Core.Entities.TransactionLog", b =>
                {
                    b.HasOne("Shop.Core.Entities.Card", "Card")
                        .WithMany("TransactionLogs")
                        .HasForeignKey("CardId");

                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("TransactionLogs")
                        .HasForeignKey("UserId");

                    b.Navigation("Card");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.HasOne("Shop.Core.Entities.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Shop.Core.Entities.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Core.Entities.Card", b =>
                {
                    b.Navigation("TransactionLogs");
                });

            modelBuilder.Entity("Shop.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Core.Entities.Discount", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Core.Entities.Invoice", b =>
                {
                    b.Navigation("InvoiceItems");
                });

            modelBuilder.Entity("Shop.Core.Entities.Product", b =>
                {
                    b.Navigation("Baskets");

                    b.Navigation("InvoiceItems");
                });

            modelBuilder.Entity("Shop.Core.Entities.User", b =>
                {
                    b.Navigation("Baskets");

                    b.Navigation("Cards");

                    b.Navigation("DeliveryAddresses");

                    b.Navigation("Invoices");

                    b.Navigation("TransactionLogs");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("Shop.Core.Entities.Wallet", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}