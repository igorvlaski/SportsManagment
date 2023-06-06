global using SportsManagment.API.Services.PlayerService;
global using SportsManagment.API.Data;
global using SportsManagment.API.Domain;
using SportsManagment.API.Services.TrainingAttendanceService;
using SportsManagment.API.Services.BodyMeasurmentsService;
using SportsManagment.API.Services.BodyMeasurmentService;
using SportsManagment.API.Services.PerformanceMeasurementService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITrainingAttendanceService, TrainingAttendanceService>();
builder.Services.AddScoped<IBodyMeasurmentService, BodyMeasurmentService>();
builder.Services.AddScoped<IPerformanceMeasurementService, PerformanceMeasurementService>();
builder.Services.AddDbContext<SportsManagmentDbContext>(optionsAction => {
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
