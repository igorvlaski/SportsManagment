﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportsManagment.API.Data;

#nullable disable

namespace SportsManagment.API.Migrations
{
    [DbContext(typeof(SportsManagmentDbContext))]
    partial class SportsManagmentDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SportsManagment.API.Domain.BodyMeasurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("ArmSpan")
                        .HasColumnType("double precision");

                    b.Property<double>("HandSpan")
                        .HasColumnType("double precision");

                    b.Property<double>("Height")
                        .HasColumnType("double precision");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<double>("ShoeSize")
                        .HasColumnType("double precision");

                    b.Property<double>("SitAndReach")
                        .HasColumnType("double precision");

                    b.Property<double>("Skinfold")
                        .HasColumnType("double precision");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("BodyMeasurements");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.PerformanceMeasurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("AgilityTest505")
                        .HasColumnType("double precision");

                    b.Property<double>("BeepTest")
                        .HasColumnType("double precision");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<double>("Sprint20m")
                        .HasColumnType("double precision");

                    b.Property<double>("VerticalJump")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PerformanceMeasurements");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsMonthlyFeePaid")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsYearlyFeePaid")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.TrainingAttendance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Selection")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("TrainingAttendances");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.BodyMeasurement", b =>
                {
                    b.HasOne("SportsManagment.API.Domain.Player", null)
                        .WithMany("BodyMeasurements")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.API.Domain.PerformanceMeasurement", b =>
                {
                    b.HasOne("SportsManagment.API.Domain.Player", null)
                        .WithMany("PerformanceMeasurements")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.API.Domain.TrainingAttendance", b =>
                {
                    b.HasOne("SportsManagment.API.Domain.Player", null)
                        .WithMany("TrainingAttendances")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Player", b =>
                {
                    b.Navigation("BodyMeasurements");

                    b.Navigation("PerformanceMeasurements");

                    b.Navigation("TrainingAttendances");
                });
#pragma warning restore 612, 618
        }
    }
}
