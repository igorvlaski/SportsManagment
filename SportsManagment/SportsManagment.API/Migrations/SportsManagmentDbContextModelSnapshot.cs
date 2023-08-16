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

            modelBuilder.Entity("PlayerSelection", b =>
                {
                    b.Property<Guid>("PlayersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SelectionsId")
                        .HasColumnType("uuid");

                    b.HasKey("PlayersId", "SelectionsId");

                    b.HasIndex("SelectionsId");

                    b.ToTable("PlayerSelection");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.MeasurementInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Location")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MeasurementInformations");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsMonthlyFeePaid")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsYearlyFeePaid")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("ParentName")
                        .HasColumnType("text");

                    b.Property<string>("ParentPhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.PlayerMeasurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AgilityTest505")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ArmSpan")
                        .HasColumnType("numeric");

                    b.Property<int>("BeepTest")
                        .HasColumnType("integer");

                    b.Property<decimal>("HandSpan")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Height")
                        .HasColumnType("numeric");

                    b.Property<Guid>("MeasurementInformationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("ShoeSize")
                        .HasColumnType("numeric");

                    b.Property<decimal>("SitAndReach")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Skinfold")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Sprint20m")
                        .HasColumnType("numeric");

                    b.Property<decimal>("VerticalJump")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MeasurementInformationId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerMeasurements");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Selection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("SelectionName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Selections");
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

                    b.Property<Guid>("SelectionId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SelectionId");

                    b.ToTable("TrainingAttendances");
                });

            modelBuilder.Entity("PlayerSelection", b =>
                {
                    b.HasOne("SportsManagment.API.Domain.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsManagment.API.Domain.Selection", null)
                        .WithMany()
                        .HasForeignKey("SelectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.API.Domain.PlayerMeasurement", b =>
                {
                    b.HasOne("SportsManagment.API.Domain.MeasurementInformation", null)
                        .WithMany("PlayerMeasurements")
                        .HasForeignKey("MeasurementInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsManagment.API.Domain.Player", null)
                        .WithMany("PlayerMeasurements")
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

                    b.HasOne("SportsManagment.API.Domain.Selection", null)
                        .WithMany("TrainingAttendances")
                        .HasForeignKey("SelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.API.Domain.MeasurementInformation", b =>
                {
                    b.Navigation("PlayerMeasurements");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Player", b =>
                {
                    b.Navigation("PlayerMeasurements");

                    b.Navigation("TrainingAttendances");
                });

            modelBuilder.Entity("SportsManagment.API.Domain.Selection", b =>
                {
                    b.Navigation("TrainingAttendances");
                });
#pragma warning restore 612, 618
        }
    }
}
