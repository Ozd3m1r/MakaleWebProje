﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories;

#nullable disable

namespace MakaleWebProje.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Models.Kategori", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriId"));

                    b.Property<string>("KategoriName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategoriId");

                    b.ToTable("Kategori");

                    b.HasData(
                        new
                        {
                            KategoriId = 1,
                            KategoriName = "Bilim Kurgu"
                        },
                        new
                        {
                            KategoriId = 2,
                            KategoriName = "Yazılım"
                        });
                });

            modelBuilder.Entity("Entities.Models.Makale", b =>
                {
                    b.Property<int>("MakaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MakaleId"));

                    b.Property<int>("KategoriId")
                        .HasColumnType("int");

                    b.Property<bool>("MakaleCarousel")
                        .HasColumnType("bit");

                    b.Property<string>("MakaleDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MakaleImagesUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MakaleIsShow")
                        .HasColumnType("bit");

                    b.Property<string>("MakaleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MakaleSummary")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MakaleId");

                    b.HasIndex("KategoriId");

                    b.ToTable("Makale");

                    b.HasData(
                        new
                        {
                            MakaleId = 1,
                            KategoriId = 1,
                            MakaleCarousel = true,
                            MakaleDate = "31.12.2013",
                            MakaleImagesUrl = "/images/1.jpg",
                            MakaleIsShow = true,
                            MakaleName = "Denem1",
                            MakaleSummary = "Makale1Özet"
                        },
                        new
                        {
                            MakaleId = 2,
                            KategoriId = 2,
                            MakaleCarousel = true,
                            MakaleDate = "26.08.2008",
                            MakaleImagesUrl = "/images/2.jpg",
                            MakaleIsShow = true,
                            MakaleName = "Denem2",
                            MakaleSummary = "Makale2Özet"
                        },
                        new
                        {
                            MakaleId = 3,
                            KategoriId = 2,
                            MakaleCarousel = true,
                            MakaleDate = "14.03.2021",
                            MakaleImagesUrl = "/images/3.jpg",
                            MakaleIsShow = true,
                            MakaleName = "Denem3",
                            MakaleSummary = "Makale3Özet"
                        });
                });

            modelBuilder.Entity("Entities.Models.MakaleData", b =>
                {
                    b.Property<int>("MakaleDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MakaleDataId"));

                    b.Property<int>("MakaleDislike")
                        .HasColumnType("int");

                    b.Property<int>("MakaleId")
                        .HasColumnType("int");

                    b.Property<int>("MakaleLike")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UsersUserId")
                        .HasColumnType("int");

                    b.HasKey("MakaleDataId");

                    b.HasIndex("MakaleId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("MakaleData");

                    b.HasData(
                        new
                        {
                            MakaleDataId = 1,
                            MakaleDislike = 1,
                            MakaleId = 1,
                            MakaleLike = 1,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Entities.Models.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<string>("UserRoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserRoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            UserRoleId = 1,
                            UserRoleName = "User"
                        },
                        new
                        {
                            UserRoleId = 2,
                            UserRoleName = "Admin"
                        });
                });

            modelBuilder.Entity("Entities.Models.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "ozdemirbaranugur@example.com",
                            Name = "Baran Uğur",
                            Password = "baranugurozdemir",
                            SurName = "Özdemir",
                            UserName = "ozdemir",
                            UserRoleId = 2
                        },
                        new
                        {
                            UserId = 2,
                            Email = "tuncyusuf@example.com",
                            Name = "Yusuf",
                            Password = "yusuftunc",
                            SurName = "Tunç",
                            UserName = "tunc",
                            UserRoleId = 2
                        },
                        new
                        {
                            UserId = 3,
                            Email = "soydincomer@example.com",
                            Name = "Ömer",
                            Password = "omersoydınc",
                            SurName = "Soydinç",
                            UserName = "soydınc",
                            UserRoleId = 2
                        },
                        new
                        {
                            UserId = 4,
                            Email = "zeynep.kaya@example.com",
                            Name = "Zeynep",
                            Password = "Password101",
                            SurName = "Kaya",
                            UserName = "zeynep",
                            UserRoleId = 1
                        });
                });

            modelBuilder.Entity("Entities.Models.Makale", b =>
                {
                    b.HasOne("Entities.Models.Kategori", "Kategori")
                        .WithMany("Makale")
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");
                });

            modelBuilder.Entity("Entities.Models.MakaleData", b =>
                {
                    b.HasOne("Entities.Models.Makale", "Makale")
                        .WithMany("MakaleDatas")
                        .HasForeignKey("MakaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Users", null)
                        .WithMany("MakaleData")
                        .HasForeignKey("UsersUserId");

                    b.Navigation("Makale");
                });

            modelBuilder.Entity("Entities.Models.Users", b =>
                {
                    b.HasOne("Entities.Models.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("Entities.Models.Kategori", b =>
                {
                    b.Navigation("Makale");
                });

            modelBuilder.Entity("Entities.Models.Makale", b =>
                {
                    b.Navigation("MakaleDatas");
                });

            modelBuilder.Entity("Entities.Models.Users", b =>
                {
                    b.Navigation("MakaleData");
                });
#pragma warning restore 612, 618
        }
    }
}