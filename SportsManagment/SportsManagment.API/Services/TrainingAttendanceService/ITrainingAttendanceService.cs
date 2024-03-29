﻿using SportsManagment.Shared.Domain;

namespace SportsManagment.API.Services.TrainingAttendanceService;

public interface ITrainingAttendanceService
{
    List<TrainingAttendance> GetAll();
    Guid Create(TrainingAttendance trainingAttendance);
    bool Delete(Guid id);
    TrainingAttendance GetById(Guid id);
    TrainingAttendance Update(Guid id,  TrainingAttendance updateTrainingAttendance);
    List<TrainingAttendance> GetAllTrainingAttendancesByPlayerId(Guid playerId, DateOnly? newerthen);
}