using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineBookStoreUser.Models
{
    public partial class Book_Store_DbContext : DbContext
    {
        public Book_Store_DbContext()
        {
        }

        public Book_Store_DbContext(DbContextOptions<Book_Store_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admins> Admins { get; set; }
        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<BookCategories> BookCategories { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<OrderBooks> OrderBooks { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Publications> Publications { get; set; }
        public virtual DbSet<Review> Review { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=TRD-510;Database=Book_Store_Db;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admins>(entity =>
            {
                entity.HasKey(e => e.AdminId);
            });

            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasKey(e => e.AuthorId);

                entity.Property(e => e.AuthorDescription).IsRequired();

                entity.Property(e => e.AuthorImage).IsRequired();

                entity.Property(e => e.AuthorName).IsRequired();
            });

            modelBuilder.Entity<BookCategories>(entity =>
            {
                entity.HasKey(e => e.BookCategoryId);

                entity.Property(e => e.BookCategoryDescription).IsRequired();

                entity.Property(e => e.BookCategoryImage).IsRequired();

                entity.Property(e => e.BookCategoryName).IsRequired();
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasKey(e => e.BookId);

                entity.HasIndex(e => e.AuthorId);

                entity.HasIndex(e => e.BookCategoryId);

                entity.HasIndex(e => e.PublicationId);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId);

                entity.HasOne(d => d.BookCategory)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.BookCategoryId);

                entity.HasOne(d => d.Publication)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.PublicationId);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.HasIndex(e => e.ReviewId);

                entity.HasIndex(e => e.UserName)
                    .IsUnique()
                    .HasFilter("([UserName] IS NOT NULL)");

                entity.HasOne(d => d.Review)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.ReviewId);
            });

            modelBuilder.Entity<OrderBooks>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.BookId });

                entity.HasIndex(e => new { e.BookId, e.OrderId })
                    .HasName("AK_OrderBooks_BookId_OrderId")
                    .IsUnique();

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.OrderBooks)
                    .HasForeignKey(d => d.BookId);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderBooks)
                    .HasForeignKey(d => d.OrderId);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.HasIndex(e => e.CustomerId);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasIndex(e => e.CustomerId);

                entity.HasIndex(e => e.OrderId)
                    .IsUnique();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.OrderId);
            });

            modelBuilder.Entity<Publications>(entity =>
            {
                entity.HasKey(e => e.PublicationId);

                entity.HasIndex(e => e.AdminId);

                entity.Property(e => e.PublicationDescription).IsRequired();

                entity.Property(e => e.PublicationImage).IsRequired();

                entity.Property(e => e.PublicationName).IsRequired();

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Publications)
                    .HasForeignKey(d => d.AdminId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.BookId);

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.BookId);
            });
        }
    }
}
