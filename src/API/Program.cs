using Application;
using Infrastructure;
using Infrastructure.Configuration;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// get MariaDbSettings from configuration
builder.Services.AddDbContext<RssReaderDbContext>(
    options =>
    {
        var mariaDbSettings = builder.Configuration.GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>();  
        var connectionString = mariaDbSettings?.ConnectionString;
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();