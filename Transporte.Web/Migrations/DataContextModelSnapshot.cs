﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Transporte.Web.Data;

namespace Transporte.Web.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Transporte.Web.Data.Entities.Afiliado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Foto");

                    b.Property<string>("Imgqr");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NroDocumento")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<int?>("SindicatoId");

                    b.HasKey("Id");

                    b.HasIndex("SindicatoId");

                    b.ToTable("Afiliados");
                });

            modelBuilder.Entity("Transporte.Web.Data.Entities.Sindicato", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("Fechafundacion");

                    b.Property<string>("Nomsindica")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Responsable")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Sindicatos");
                });

            modelBuilder.Entity("Transporte.Web.Data.Entities.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AfiliadoId");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Nrochasis")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Nromotor")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Nroplaca")
                        .IsRequired()
                        .HasMaxLength(8);

                    b.HasKey("Id");

                    b.HasIndex("AfiliadoId");

                    b.HasIndex("Nroplaca")
                        .IsUnique();

                    b.ToTable("Vehiculos");
                });

            modelBuilder.Entity("Transporte.Web.Data.Entities.Afiliado", b =>
                {
                    b.HasOne("Transporte.Web.Data.Entities.Sindicato", "Sindicato")
                        .WithMany("Afiliados")
                        .HasForeignKey("SindicatoId");
                });

            modelBuilder.Entity("Transporte.Web.Data.Entities.Vehiculo", b =>
                {
                    b.HasOne("Transporte.Web.Data.Entities.Afiliado", "Afiliado")
                        .WithMany("Vehiculos")
                        .HasForeignKey("AfiliadoId");
                });
#pragma warning restore 612, 618
        }
    }
}
