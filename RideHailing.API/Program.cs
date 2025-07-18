using RideHailing.API.Hubs;
using RideHailing.API.Middleware;
using RideHailing.Application;
using RideHailing.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddSignalRCore();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontened",
        policy =>
        {
            policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials().AllowAnyOrigin();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Frontened");

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.MapHub<LocationHub>("/location");

app.Run();
