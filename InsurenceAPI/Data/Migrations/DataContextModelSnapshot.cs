﻿// <auto-generated />
using InsurenceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InsurenceAPI.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("InsurenceAPI.Entities.Occupation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RatingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.ToTable("Occupations");
                });

            modelBuilder.Entity("InsurenceAPI.Entities.RatingFactor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Factor")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RatingFactors");
                });

            modelBuilder.Entity("InsurenceAPI.Entities.Occupation", b =>
                {
                    b.HasOne("InsurenceAPI.Entities.RatingFactor", "Rating")
                        .WithMany()
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");
                });
#pragma warning restore 612, 618
        }
    }
}
