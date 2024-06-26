﻿// <auto-generated />
using System;
using Index_Libri.Server.BLL.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Index_Libri.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240418170833_Index-Libri2")]
    partial class IndexLibri2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("BookListId")
                        .HasColumnType("integer");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Pages")
                        .HasColumnType("integer");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.HasKey("BookId");

                    b.HasIndex("BookListId");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("BookList", b =>
                {
                    b.Property<int>("BookListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("BookListId"));

                    b.Property<int>("ApplicationUserId")
                        .HasColumnType("integer");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("BookListId");

                    b.HasIndex("ApplicationUserId")
                        .IsUnique();

                    b.ToTable("BookList");
                });

            modelBuilder.Entity("Index_Libri.Server.BLL.Model.ApplicationUser", b =>
                {
                    b.Property<int>("dbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("dbId"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("dbId");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Book", b =>
                {
                    b.HasOne("BookList", "BookList")
                        .WithMany("Books")
                        .HasForeignKey("BookListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookList");
                });

            modelBuilder.Entity("BookList", b =>
                {
                    b.HasOne("Index_Libri.Server.BLL.Model.ApplicationUser", "ApplicationUser")
                        .WithOne("BookList")
                        .HasForeignKey("BookList", "ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("BookList", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Index_Libri.Server.BLL.Model.ApplicationUser", b =>
                {
                    b.Navigation("BookList")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
