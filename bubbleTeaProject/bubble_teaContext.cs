using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace bubbleTeaProject
{
    public partial class bubble_teaContext : DbContext
    {
        public bubble_teaContext()
        {
        }

        public bubble_teaContext(DbContextOptions<bubble_teaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Ordering> Orderings { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductsInOrder> ProductsInOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bubble_tea;Username=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.TelNum)
                    .HasName("customer_pkey");

                entity.ToTable("customer");

                entity.Property(e => e.TelNum)
                    .ValueGeneratedNever()
                    .HasColumnName("tel_num");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Ordering>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("ordering_pkey");

                entity.ToTable("ordering");

                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.OrderPrice)
                    .HasColumnType("money")
                    .HasColumnName("order_price");

                entity.Property(e => e.TelNum).HasColumnName("tel_num");

                entity.HasOne(d => d.TelNumNavigation)
                    .WithMany(p => p.Orderings)
                    .HasForeignKey(d => d.TelNum)
                    .HasConstraintName("ordering_tel_num_fkey");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProdId)
                    .HasName("product_pkey");

                entity.ToTable("product");

                entity.Property(e => e.ProdId)
                    .HasColumnName("prod_id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.ProdName)
                    .HasMaxLength(50)
                    .HasColumnName("prod_name")
                    .IsFixedLength();

                entity.Property(e => e.ProdPrice)
                    .HasColumnType("money")
                    .HasColumnName("prod_price");
            });

            modelBuilder.Entity<ProductsInOrder>(entity =>
            {
                entity.HasKey(e => new { e.ProdId, e.OrderId })
                    .HasName("pk_prod_in_order");

                entity.ToTable("products_in_order");

                entity.Property(e => e.ProdId).HasColumnName("prod_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("1");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductsInOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_in_order_order_id_fkey");

                entity.HasOne(d => d.Prod)
                    .WithMany(p => p.ProductsInOrders)
                    .HasForeignKey(d => d.ProdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("products_in_order_prod_id_fkey");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
