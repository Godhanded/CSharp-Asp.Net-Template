using CSharp_Asp.Net_Template.Application;
using CSharp_Asp.Net_Template.Infrastructure;
using CSharp_Asp.Net_Template.Web.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilogConfiguredLogger();

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureConfig(builder.Configuration);
builder.Services.AddApplicationConfig(builder.Configuration);

var app = builder.Build();
await app.MigrateAndSeedDataBase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/BoilerPlate/swagger.json", "My API V1");
    //    c.RoutePrefix = "/docs";
    //});
}

app.UseCors();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
