using Application;
using Infrastructure;
using Infrastructure.Utilities;
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

// InMemory Database for testing
builder.Services.AddDbContext<RssReaderDbContext>(options => 
options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("MyTestDb")));

// get MariaDbSettings from configuration
// builder.Services.AddDbContext<RssReaderDbContext>(
//     options =>
//     {
//         var mariaDbSettings = builder.Configuration.GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>();  
//         var connectionString = mariaDbSettings?.ConnectionString;
//         options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//     });

builder.Services.AddCors(options =>
{
options.AddPolicy("AllowLocalhost",
policy =>
{
policy.WithOrigins("http://localhost:44492")
.AllowAnyHeader()
.AllowAnyMethod();
});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.MapControllers();

// using ( var scope = app.Services.CreateScope())
// {
//     var db = scope.ServiceProvider.GetRequiredService<RssReaderDbContext>();
//     db.Database.Migrate();
// }

app.Run();