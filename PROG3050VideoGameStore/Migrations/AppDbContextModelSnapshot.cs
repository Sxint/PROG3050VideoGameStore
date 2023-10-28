﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PROG3050VideoGameStore.Models;

#nullable disable

namespace PROG3050VideoGameStore.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfEvent")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.ProfilePreferences", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("FavCategory")
                        .HasColumnType("int");

                    b.Property<int>("FavPlatform")
                        .HasColumnType("int");

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("ProfilePreferencesList");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int>("RatingValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Review");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AptNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AptNumberShipping")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CityShipping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeliveryInstructions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCodeShipping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProvinceShipping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SameAsShipping")
                        .HasColumnType("bit");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddressShipping")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.ToTable("UserAddresses");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActualName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailValidate")
                        .HasColumnType("bit");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEmployee")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("PromotionEmail")
                        .HasColumnType("bit");

                    b.Property<bool?>("RememberMe")
                        .HasColumnType("bit");

                    b.Property<int>("RepeatedInvalidCreds")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.ProfilePreferences", b =>
                {
                    b.HasOne("PROG3050VideoGameStore.Models.UserProfile", "Profile")
                        .WithOne("Preferences")
                        .HasForeignKey("PROG3050VideoGameStore.Models.ProfilePreferences", "UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Rating", b =>
                {
                    b.HasOne("PROG3050VideoGameStore.Models.Game", "Game")
                        .WithOne("RatingItem")
                        .HasForeignKey("PROG3050VideoGameStore.Models.Rating", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROG3050VideoGameStore.Models.UserProfile", "Profile")
                        .WithOne("RatingItem")
                        .HasForeignKey("PROG3050VideoGameStore.Models.Rating", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Review", b =>
                {
                    b.HasOne("PROG3050VideoGameStore.Models.Game", "Game")
                        .WithOne("ReviewItem")
                        .HasForeignKey("PROG3050VideoGameStore.Models.Review", "GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PROG3050VideoGameStore.Models.UserProfile", "Profile")
                        .WithOne("ReviewItem")
                        .HasForeignKey("PROG3050VideoGameStore.Models.Review", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.UserAddress", b =>
                {
                    b.HasOne("PROG3050VideoGameStore.Models.UserProfile", "Profile")
                        .WithOne("Address")
                        .HasForeignKey("PROG3050VideoGameStore.Models.UserAddress", "UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.Game", b =>
                {
                    b.Navigation("RatingItem");

                    b.Navigation("ReviewItem");
                });

            modelBuilder.Entity("PROG3050VideoGameStore.Models.UserProfile", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Preferences");

                    b.Navigation("RatingItem");

                    b.Navigation("ReviewItem");
                });
#pragma warning restore 612, 618
        }
    }
}
