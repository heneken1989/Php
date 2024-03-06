﻿// <auto-generated />
using System;
using Group5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Group5.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20291115151655_seed")]
    partial class seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Group5.Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("Role");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Group5.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Group5.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FromUserId")
                        .HasColumnType("int");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToRoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToRoomId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Group5.Models.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.HasKey("Name");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Group5.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Group5.Data.Employee", b =>
                {
                    b.HasOne("Group5.Models.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("Group5.Models.Role", "Roletable")
                        .WithMany()
                        .HasForeignKey("Role");

                    b.Navigation("Departments");

                    b.Navigation("Roletable");
                });

            modelBuilder.Entity("Group5.Models.Message", b =>
                {
                    b.HasOne("Group5.Data.Employee", "FromUser")
                        .WithMany("Messages")
                        .HasForeignKey("FromUserId");

                    b.HasOne("Group5.Models.Room", "ToRoom")
                        .WithMany("Messages")
                        .HasForeignKey("ToRoomId");

                    b.Navigation("FromUser");

                    b.Navigation("ToRoom");
                });

            modelBuilder.Entity("Group5.Models.Room", b =>
                {
                    b.HasOne("Group5.Data.Employee", "Admin")
                        .WithMany("Rooms")
                        .HasForeignKey("AdminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Group5.Data.Employee", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Group5.Models.Room", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}
