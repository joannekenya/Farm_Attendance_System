﻿// <auto-generated />
using System;
using FARM_ATTENDANCE_SYSTEM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FARM_ATTENDANCE_SYSTEM.Migrations
{
    [DbContext(typeof(FarmDbContext))]
    [Migration("20250425120027_UpdateUserModel")]
    partial class UpdateUserModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FARM_ATTENDANCE_SYSTEM.Models.Farmers", b =>
                {
                    b.Property<int>("FarmerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FarmerId"));

                    b.Property<int>("AGE")
                        .HasColumnType("int");

                    b.Property<decimal>("AMOUNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("County")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("FIELDCOORDINATOR")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FPO_COOP_Group")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FPhoneNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FarmerName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("GENDER")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("IDNO")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsManaged")
                        .HasColumnType("bit");

                    b.Property<bool>("ListofFarmers")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<string>("SubCounty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VENUE")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FarmerId");

                    b.ToTable("Farmers");
                });

            modelBuilder.Entity("FARM_ATTENDANCE_SYSTEM.Models.Locations", b =>
                {
                    b.Property<int>("Position")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Position"));

                    b.Property<string>("StrCompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrCompanyNameId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrCounty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrCountyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrSubCounty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrSubCountyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrWard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StrWardId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Position");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("FARM_ATTENDANCE_SYSTEM.Models.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("UserGroup")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("FARM_ATTENDANCE_SYSTEM.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserGroup")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
