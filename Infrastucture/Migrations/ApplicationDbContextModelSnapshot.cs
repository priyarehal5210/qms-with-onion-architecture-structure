﻿// <auto-generated />
using Infrastucture.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastucture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DomainLayer.Entities.AssignTasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<bool>("isChecked")
                        .HasColumnType("bit");

                    b.Property<int>("registeredUsersId")
                        .HasColumnType("int");

                    b.Property<int>("tasksId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("registeredUsersId");

                    b.HasIndex("tasksId");

                    b.ToTable("assignTasks");
                });

            modelBuilder.Entity("DomainLayer.Entities.RegisteredUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("approved")
                        .HasColumnType("bit");

                    b.Property<string>("confirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("emailConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("registeredUsers");
                });

            modelBuilder.Entity("DomainLayer.Entities.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("endDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("startDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("tasks");
                });

            modelBuilder.Entity("DomainLayer.Entities.UserSuccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("assignTasksId")
                        .HasColumnType("int");

                    b.Property<string>("date")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("hours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("success")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("assignTasksId");

                    b.ToTable("userSuccesses");
                });

            modelBuilder.Entity("DomainLayer.Entities.AssignTasks", b =>
                {
                    b.HasOne("DomainLayer.Entities.RegisteredUsers", "registeredUsers")
                        .WithMany()
                        .HasForeignKey("registeredUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DomainLayer.Entities.Tasks", "tasks")
                        .WithMany()
                        .HasForeignKey("tasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("registeredUsers");

                    b.Navigation("tasks");
                });

            modelBuilder.Entity("DomainLayer.Entities.UserSuccess", b =>
                {
                    b.HasOne("DomainLayer.Entities.AssignTasks", "assignTasks")
                        .WithMany()
                        .HasForeignKey("assignTasksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("assignTasks");
                });
#pragma warning restore 612, 618
        }
    }
}