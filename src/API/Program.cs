using Application;
using Infrastructure;
using Infrastructure.Configuration;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.Configure<MariaDbSettings>(builder.Configuration.GetSection("MariaDbSettings"));
// builder.Services.AddSingleton<MariaDbSettings>((s) => 
//     s.GetService<IConfiguration>().GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>());

// See: https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/
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






