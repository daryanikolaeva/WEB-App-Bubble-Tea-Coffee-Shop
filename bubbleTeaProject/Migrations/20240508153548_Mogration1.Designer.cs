﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using bubbleTeaProject;

#nullable disable

namespace bubbleTeaProject.Migrations
{
    [DbContext(typeof(bubble_teaContext))]
    [Migration("20240508153548_Mogration1")]
    partial class Mogration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("bubbleTeaProject.Customer", b =>
                {
                    b.Property<int>("TelNum")
                        .HasColumnType("integer")
                        .HasColumnName("tel_num");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("character(20)")
                        .HasColumnName("name")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("character(100)")
                        .HasColumnName("password")
                        .IsFixedLength();

                    b.HasKey("TelNum")
                        .HasName("customer_pkey");

                    b.ToTable("customer", (string)null);
                });

            modelBuilder.Entity("bubbleTeaProject.Ordering", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("OrderId"));

                    b.Property<decimal?>("OrderPrice")
                        .HasColumnType("money")
                        .HasColumnName("order_price");

                    b.Property<int?>("TelNum")
                        .HasColumnType("integer")
                        .HasColumnName("tel_num");

                    b.HasKey("OrderId")
                        .HasName("ordering_pkey");

                    b.HasIndex("TelNum");

                    b.ToTable("ordering", (string)null);
                });

            modelBuilder.Entity("bubbleTeaProject.Product", b =>
                {
                    b.Property<int>("ProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("prod_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityAlwaysColumn(b.Property<int>("ProdId"));

                    b.Property<string>("ProdName")
                        .HasMaxLength(50)
                        .HasColumnType("character(50)")
                        .HasColumnName("prod_name")
                        .IsFixedLength();

                    b.Property<decimal?>("ProdPrice")
                        .HasColumnType("money")
                        .HasColumnName("prod_price");

                    b.HasKey("ProdId")
                        .HasName("product_pkey");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("bubbleTeaProject.ProductsInOrder", b =>
                {
                    b.Property<int>("ProdId")
                        .HasColumnType("integer")
                        .HasColumnName("prod_id");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("order_id");

                    b.Property<int?>("Amount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("amount")
                        .HasDefaultValueSql("1");

                    b.HasKey("ProdId", "OrderId")
                        .HasName("pk_prod_in_order");

                    b.HasIndex("OrderId");

                    b.ToTable("products_in_order", (string)null);
                });

            modelBuilder.Entity("bubbleTeaProject.Ordering", b =>
                {
                    b.HasOne("bubbleTeaProject.Customer", "TelNumNavigation")
                        .WithMany("Orderings")
                        .HasForeignKey("TelNum")
                        .HasConstraintName("ordering_tel_num_fkey");

                    b.Navigation("TelNumNavigation");
                });

            modelBuilder.Entity("bubbleTeaProject.ProductsInOrder", b =>
                {
                    b.HasOne("bubbleTeaProject.Ordering", "Order")
                        .WithMany("ProductsInOrders")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("products_in_order_order_id_fkey");

                    b.HasOne("bubbleTeaProject.Product", "Prod")
                        .WithMany("ProductsInOrders")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("products_in_order_prod_id_fkey");

                    b.Navigation("Order");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("bubbleTeaProject.Customer", b =>
                {
                    b.Navigation("Orderings");
                });

            modelBuilder.Entity("bubbleTeaProject.Ordering", b =>
                {
                    b.Navigation("ProductsInOrders");
                });

            modelBuilder.Entity("bubbleTeaProject.Product", b =>
                {
                    b.Navigation("ProductsInOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
