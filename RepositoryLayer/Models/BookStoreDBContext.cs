using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RepoLayer.Models
{
    public partial class BookStoreDBContext : DbContext
    {
        public BookStoreDBContext()
        {
        }

        public BookStoreDBContext(DbContextOptions<BookStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressTable> AddressTable { get; set; }
        public virtual DbSet<BookTable> BookTable { get; set; }
        public virtual DbSet<OrderTable> OrderTable { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressTable>(entity =>
            {
                entity.HasKey(e => e.AddressId)
                    .HasName("PK__AddressT__091C2A1BF1271495");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserAddress)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AddressTable)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__AddressTa__UserI__3F466844");
            });

            modelBuilder.Entity<BookTable>(entity =>
            {
                entity.HasKey(e => e.BookId)
                    .HasName("PK__BookTabl__3DE0C22752F58F48");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.AuthorName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BookDescription).IsUnicode(false);

                entity.Property(e => e.BookImage)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.BookName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderTable>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderTab__C3905BAFEB761C27");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("FK__OrderTabl__Addre__403A8C7D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderTable)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__OrderTabl__UserI__3C69FB99");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserTabl__1788CCAC00964F3F");

                entity.HasIndex(e => e.UserEmailId)
                    .HasName("UQ__UserTabl__09C7B4CCF600BE7C")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.UserEmailId)
                    .HasColumnName("UserEmailID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserPhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserRole)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            Console.WriteLine("Hello, I am King Kholi.");
        }
    }
}
