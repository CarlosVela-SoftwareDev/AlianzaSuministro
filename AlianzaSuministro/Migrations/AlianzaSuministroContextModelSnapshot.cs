﻿// <auto-generated />
using System;
using AlianzaSuministro.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AlianzaSuministro.Migrations
{
    [DbContext(typeof(AlianzaSuministroContext))]
    partial class AlianzaSuministroContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AlianzaSuministro.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EsActivo")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal?>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TipoProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoProductoId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.ProductoProveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaveProductoProveedor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Costo")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("ProductoProveedor");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.Proveedor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Proveedor");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.TipoProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("TipoProducto");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.Producto", b =>
                {
                    b.HasOne("AlianzaSuministro.Models.TipoProducto", "TipoProducto")
                        .WithMany("Productos")
                        .HasForeignKey("TipoProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoProducto");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.ProductoProveedor", b =>
                {
                    b.HasOne("AlianzaSuministro.Models.Producto", "Producto")
                        .WithMany("ProductoProveedores")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlianzaSuministro.Models.Proveedor", "Proveedor")
                        .WithMany("ProductoProveedores")
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.Producto", b =>
                {
                    b.Navigation("ProductoProveedores");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.Proveedor", b =>
                {
                    b.Navigation("ProductoProveedores");
                });

            modelBuilder.Entity("AlianzaSuministro.Models.TipoProducto", b =>
                {
                    b.Navigation("Productos");
                });
#pragma warning restore 612, 618
        }
    }
}