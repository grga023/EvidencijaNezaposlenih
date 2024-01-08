﻿// <auto-generated />
using System;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EvidencijaNezaposlenih.Repozitorijum.Migrations
{
    [DbContext(typeof(EvidencijaNezaposlenihDBContext))]
    partial class EvidencijaNezaposlenihDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EvidencijaNezaposlenih.ModeliPodataka.Modeli.Nezaposleni", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrojTelefona")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRodjenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JMBG")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Nezaposleni");
                });

            modelBuilder.Entity("EvidencijaNezaposlenih.ModeliPodataka.Modeli.Poslodavac", b =>
                {
                    b.Property<Guid>("PIB")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Adresa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PIB");

                    b.ToTable("Poslodavci");
                });

            modelBuilder.Entity("EvidencijaNezaposlenih.ModeliPodataka.Modeli.RadniOdnos", b =>
                {
                    b.Property<Guid>("PIB")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NezaposleniID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Trajanje")
                        .HasColumnType("int");

                    b.HasKey("PIB", "NezaposleniID");

                    b.HasIndex("NezaposleniID");

                    b.ToTable("RadniOdnosi");
                });

            modelBuilder.Entity("EvidencijaNezaposlenih.ModeliPodataka.Modeli.RadniOdnos", b =>
                {
                    b.HasOne("EvidencijaNezaposlenih.ModeliPodataka.Modeli.Nezaposleni", "Nezaposleni")
                        .WithMany("RadniOdnos")
                        .HasForeignKey("NezaposleniID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EvidencijaNezaposlenih.ModeliPodataka.Modeli.Poslodavac", "Poslodavac")
                        .WithMany()
                        .HasForeignKey("PIB")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nezaposleni");

                    b.Navigation("Poslodavac");
                });

            modelBuilder.Entity("EvidencijaNezaposlenih.ModeliPodataka.Modeli.Nezaposleni", b =>
                {
                    b.Navigation("RadniOdnos");
                });
#pragma warning restore 612, 618
        }
    }
}
