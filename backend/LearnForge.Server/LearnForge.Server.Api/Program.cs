using LearnForge.Server.Api.Data.Context;
using LearnForge.Server.Api.Data.Repositories.Interfaces;
using LearnForge.Server.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDapperContext, DapperContext>();

// configurations
builder.Services.Configure<ConnectionStringSettings>(builder.Configuration.GetSection("ConnectionStrings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
