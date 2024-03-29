﻿namespace SportsManagment.Shared.Domain;

public class Player
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ParentName { get; set; }
    public string? ParentPhoneNumber { get; set; }
    public bool IsDeleted { get; set; } = false;
    public List<TrainingAttendance> TrainingAttendances { get; set; } = new List<TrainingAttendance>();
    public List<PlayerMeasurement> PlayerMeasurements { get; set; } = new List<PlayerMeasurement>();
    public List<Selection> Selections { get; set; } = new List<Selection>();
    public List<PaymentInformation> PaymentInformations { get; set; } = new List<PaymentInformation>();
}