﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Cenovnik.Migrations
{
    [DbContext(typeof(ProdavnicaContext))]
    [Migration("20220122213746_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Drzava", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Drzava");

                    b.HasKey("ID");

                    b.ToTable("Drzava");
                });

            modelBuilder.Entity("Models.Grad", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Grad");

                    b.HasKey("ID");

                    b.ToTable("Grad");
                });

            modelBuilder.Entity("Models.Predmet", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Cena")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Predmet");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DrzavaID")
                        .HasColumnType("int");

                    b.Property<int?>("GradID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("DrzavaID");

                    b.HasIndex("GradID");

                    b.ToTable("Prodavnica");
                });

            modelBuilder.Entity("Models.Storage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PredmetID")
                        .HasColumnType("int");

                    b.Property<int>("ProdavnicaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PredmetID");

                    b.HasIndex("ProdavnicaID");

                    b.ToTable("Storage");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.HasOne("Models.Drzava", "Drzava")
                        .WithMany()
                        .HasForeignKey("DrzavaID");

                    b.HasOne("Models.Grad", "Grad")
                        .WithMany()
                        .HasForeignKey("GradID");

                    b.Navigation("Drzava");

                    b.Navigation("Grad");
                });

            modelBuilder.Entity("Models.Storage", b =>
                {
                    b.HasOne("Models.Predmet", "Predmet")
                        .WithMany("Storages")
                        .HasForeignKey("PredmetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Prodavnica", "Prodavnica")
                        .WithMany("Storages")
                        .HasForeignKey("ProdavnicaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Predmet");

                    b.Navigation("Prodavnica");
                });

            modelBuilder.Entity("Models.Predmet", b =>
                {
                    b.Navigation("Storages");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Storages");
                });
#pragma warning restore 612, 618
        }
    }
}