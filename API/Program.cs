using System.Text;
using API.Data;
using API.Extensions;
using API.Helper;
using API.Interface;
using API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // <-- Temporary logger for startup

// Setup Serilog from configuration
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext();
});


// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.Register();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddEndpointsApiExplorer(); // <-- Required for Swagger to see endpoints
builder.Services.AddSwaggerGen();           // <-- Generates Swagger UI and JSON
var app = builder.Build();



    app.UseSwagger();
    app.UseSwaggerUI();

// Configure the HTTP request pipeline.
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
ImageHelper.Initialize(app.Environment);
app.Run();
