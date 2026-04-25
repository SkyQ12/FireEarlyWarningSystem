using Microsoft.EntityFrameworkCore;
using FireEarlyWarningSystem.Infrastructure.Domain.Context;
using FireEarlyWarningSystem.Infrastructure.Services.Users;
using SmartBin.Infrastructure.Repositories.UnitOfWork;
using FireEarlyWarningSystem.Infrastructure.Repositories.Users;
using FireEarlyWarningSystem.Infrastructure.Domain.Mapping;
using FireEarlyWarningSystem.Infrastructure.Domain.Resources.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .WithOrigins("http://localhost:3003", "http://localhost:3002", "http://localhost:3001", "http://localhost:3000", "http://localhost:7144",
                         "http://localhost:3007", "http://localhost:3008", "http://localhost:3009", "http://localhost:3010",
                         "https://web-smart-bin.vercel.app", "https://smart-bin-gps.vercel.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("FireEarlyWarningSystem"))
);

// Add Scoped Services

// Login
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };

        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                var path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/NotificationHub"))
                {
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

// Service
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));
builder.Services.AddAutoMapper(typeof(ViewModelToModelProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}







app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
