﻿// <auto-generated />
using HopHubApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HopHubApi.Migrations
{
    [DbContext(typeof(ApiContext))]
    [Migration("20191024175621_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("HopHubApi.Models.Beer", b =>
                {
                    b.Property<int>("BeerId")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Abv");

                    b.Property<string>("Brewery")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Style")
                        .IsRequired();

                    b.HasKey("BeerId");

                    b.ToTable("Beers");
                });

            modelBuilder.Entity("HopHubApi.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BeerId");

                    b.Property<string>("Comments")
                        .IsRequired();

                    b.Property<string>("DrinkAgain")
                        .IsRequired();

                    b.HasKey("ReviewId");

                    b.HasIndex("BeerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HopHubApi.Models.Review", b =>
                {
                    b.HasOne("HopHubApi.Models.Beer")
                        .WithMany("Reviews")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
