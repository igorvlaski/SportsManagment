﻿global using Microsoft.EntityFrameworkCore;
using SportsManagment.Shared.Domain;

namespace SportsManagment.API.Data;

public class SportsManagmentDbContext : DbContext
{
    public SportsManagmentDbContext(DbContextOptions<SportsManagmentDbContext> options) : base(options)
    {           
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);  
    }
    public DbSet<Player> Players { get; set; }
    public DbSet<TrainingAttendance> TrainingAttendances { get; set; }
    public DbSet<PlayerMeasurement> PlayerMeasurements { get; set; }
    public DbSet<MeasurementInformation> MeasurementInformations { get; set; }
    public DbSet<Selection> Selections { get; set; }
    public DbSet<PaymentInformation> PaymentInformations { get; set; }
}