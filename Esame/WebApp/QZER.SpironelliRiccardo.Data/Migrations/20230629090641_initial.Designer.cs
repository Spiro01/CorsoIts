﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QZER.SpironelliRiccardo.Data.Persistence;

#nullable disable

namespace QZER.SpironelliRiccardo.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230629090641_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Address")
                        .HasColumnType("smallint");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid>("GatewayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GatewayId");

                    b.ToTable("Board");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Building", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Gateway", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuildingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("FloorNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuildingId");

                    b.ToTable("Gateway");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Board", b =>
                {
                    b.HasOne("QZER.SpironelliRiccardo.Data.Entities.Gateway", "Gateway")
                        .WithMany("Boards")
                        .HasForeignKey("GatewayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gateway");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Gateway", b =>
                {
                    b.HasOne("QZER.SpironelliRiccardo.Data.Entities.Building", "Building")
                        .WithMany("Gateway")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Building", b =>
                {
                    b.Navigation("Gateway");
                });

            modelBuilder.Entity("QZER.SpironelliRiccardo.Data.Entities.Gateway", b =>
                {
                    b.Navigation("Boards");
                });
#pragma warning restore 612, 618
        }
    }
}
