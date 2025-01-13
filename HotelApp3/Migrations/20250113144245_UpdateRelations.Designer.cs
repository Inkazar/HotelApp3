﻿// <auto-generated />
using System;
using HotelApp3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HotelApp3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250113144245_UpdateRelations")]
    partial class UpdateRelations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HotelApp3.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExtraBeds")
                        .HasColumnType("int");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("HotelApp3.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "alice@example.com",
                            Name = "Alice Smith",
                            Phone = "123456789"
                        },
                        new
                        {
                            Id = 2,
                            Email = "bob@example.com",
                            Name = "Bob Johnson",
                            Phone = "987654321"
                        },
                        new
                        {
                            Id = 3,
                            Email = "charlie@example.com",
                            Name = "Charlie Brown",
                            Phone = "456789123"
                        },
                        new
                        {
                            Id = 4,
                            Email = "diana@example.com",
                            Name = "Diana Ross",
                            Phone = "789123456"
                        });
                });

            modelBuilder.Entity("HotelApp3.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExtraBeds")
                        .HasColumnType("int");

                    b.Property<int>("MaxCapacity")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerNight")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExtraBeds = 0,
                            MaxCapacity = 1,
                            PricePerNight = 500m,
                            Type = "Single"
                        },
                        new
                        {
                            Id = 2,
                            ExtraBeds = 0,
                            MaxCapacity = 2,
                            PricePerNight = 800m,
                            Type = "Double"
                        },
                        new
                        {
                            Id = 3,
                            ExtraBeds = 0,
                            MaxCapacity = 2,
                            PricePerNight = 900m,
                            Type = "Double"
                        },
                        new
                        {
                            Id = 4,
                            ExtraBeds = 0,
                            MaxCapacity = 4,
                            PricePerNight = 1200m,
                            Type = "Double"
                        });
                });

            modelBuilder.Entity("HotelApp3.Models.Booking", b =>
                {
                    b.HasOne("HotelApp3.Models.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("HotelApp3.Models.Customer", b =>
                {
                    b.Navigation("Bookings");
                });
#pragma warning restore 612, 618
        }
    }
}
