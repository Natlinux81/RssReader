using System.Text;
using Application.Extensions;
using Infrastructure;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero // remove delay of token expiration time
        };
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