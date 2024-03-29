﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportsManagment.API.Data;

#nullable disable

namespace SportsManagment.API.Migrations;

[DbContext(typeof(SportsManagmentDbContext))]
[Migration("20230531171541_RemoveWasPresent")]
partial class RemoveWasPresent
{
    /// <inheritdoc />
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.5")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

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
                b.Navigation("TrainingAttendances");
            });
#pragma warning restore 612, 618
    }
}
