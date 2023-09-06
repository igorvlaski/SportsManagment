global using SportsManagment.API.Services.PlayerService;
global using SportsManagment.API.Data;
global using SportsManagment.API.Domain;
using SportsManagment.API.Services.TrainingAttendanceService;
using SportsManagment.API.Services.PlayerMeasurementService;
using SportsManagment.API.Services.MeasurementInformationService;
using SportsManagment.API.Services.SelectionService;
using System.Text.Json.Serialization;
using SportsManagment.API.Services.PaymentInformationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<ITrainingAttendanceService, TrainingAttendanceService>();
builder.Services.AddScoped<IPlayerMeasurementService, PlayerMeasurementService>();
builder.Services.AddScoped<IMeasurementInformationService, MeasurementInformationService>();
builder.Services.AddScoped<ISelectionService, SelectionService>();
builder.Services.AddScoped<IPaymentInformationService, PaymentInformationService>();

builder.Services.AddDbContext<SportsManagmentDbContext>(optionsAction =>
{
    optionsAction.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.UseAuthorization();

app.MapControllers();

app.Run();
