﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportsManagment.API.Data;

#nullable disable

namespace SportsManagment.API.Migrations
{
    [DbContext(typeof(SportsManagmentDbContext))]
    [Migration("20240108111755_ChangeAmountToDecimal")]
    partial class ChangeAmountToDecimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
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

            modelBuilder.Entity("SportsManagment.Shared.Domain.MeasurementInformation", b =>
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

            modelBuilder.Entity("SportsManagment.Shared.Domain.PaymentInformation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("DateOfPayment")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("PlayerId")
                        .HasColumnType("uuid");

                    b.Property<int>("typeOfPayment")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("PaymentInformations");
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.Player", b =>
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
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
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

            modelBuilder.Entity("SportsManagment.Shared.Domain.PlayerMeasurement", b =>
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

            modelBuilder.Entity("SportsManagment.Shared.Domain.Selection", b =>
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

            modelBuilder.Entity("SportsManagment.Shared.Domain.TrainingAttendance", b =>
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
                    b.HasOne("SportsManagment.Shared.Domain.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsManagment.Shared.Domain.Selection", null)
                        .WithMany()
                        .HasForeignKey("SelectionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.PaymentInformation", b =>
                {
                    b.HasOne("SportsManagment.Shared.Domain.Player", null)
                        .WithMany("PaymentInformations")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.PlayerMeasurement", b =>
                {
                    b.HasOne("SportsManagment.Shared.Domain.MeasurementInformation", null)
                        .WithMany("PlayerMeasurements")
                        .HasForeignKey("MeasurementInformationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsManagment.Shared.Domain.Player", null)
                        .WithMany("PlayerMeasurements")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.TrainingAttendance", b =>
                {
                    b.HasOne("SportsManagment.Shared.Domain.Player", null)
                        .WithMany("TrainingAttendances")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportsManagment.Shared.Domain.Selection", null)
                        .WithMany("TrainingAttendances")
                        .HasForeignKey("SelectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.MeasurementInformation", b =>
                {
                    b.Navigation("PlayerMeasurements");
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.Player", b =>
                {
                    b.Navigation("PaymentInformations");

                    b.Navigation("PlayerMeasurements");

                    b.Navigation("TrainingAttendances");
                });

            modelBuilder.Entity("SportsManagment.Shared.Domain.Selection", b =>
                {
                    b.Navigation("TrainingAttendances");
                });
#pragma warning restore 612, 618
        }
    }
}