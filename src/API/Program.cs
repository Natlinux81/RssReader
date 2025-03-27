using System.Text;
using API;
using Application.Extensions;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddWebServices(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    // MariaDb Database for development
    builder.Services.AddDbContext<RssReaderDbContext>(
        options =>
        {
            var mariaDbSettings = builder.Configuration.GetRequiredSection("MariaDbSettings").Get<MariaDbSettings>();
            var connectionString = mariaDbSettings?.ConnectionString;
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });
}
else
{
    // InMemory Database for testing
    var dbName = builder.Configuration.GetConnectionString("MyTestDb");

    if (string.IsNullOrEmpty(dbName))
        throw new ArgumentNullException(nameof(dbName), "Database name should not be null or empty.");

    builder.Services.AddDbContext<RssReaderDbContext>(options =>
        options.UseInMemoryDatabase(dbName));
}

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
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.MapControllers();
app.UseStaticFiles();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<RssReaderDbContext>();
    context.Database.EnsureCreated();
}

app.Run();

public class LowerCaseDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        // get the paths
        var paths = swaggerDoc.Paths.ToDictionary(
            path => path.Key.ToLowerInvariant(),
            path => swaggerDoc.Paths[path.Key]);
        
        // add the paths
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var pathItem in paths)
        {
            swaggerDoc.Paths.Add(pathItem.Key, pathItem.Value);
        }
    }
}