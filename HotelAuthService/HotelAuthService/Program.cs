using HotelAuthService.Data;
using HotelAuthService.Middleware;
using HotelAuthService.Repositories;
using HotelAuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

namespace HotelAuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    "Logs/hotelauth-log.txt",
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("HotelAuthService starting up");

                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();

                builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseMySql(
                        builder.Configuration
                            .GetConnectionString("DefaultConnection"),
                        ServerVersion.AutoDetect(
                            builder.Configuration
                                .GetConnectionString("DefaultConnection"))));

                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtSettings = builder.Configuration
                        .GetSection("JwtSettings");
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings["Issuer"],
                            ValidAudience = jwtSettings["Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    jwtSettings["SecretKey"]!))
                        };
                });

                builder.Services.AddAuthorization();

                builder.Services.AddScoped<IUserRepository,
                    UserRepository>();
                builder.Services.AddScoped<IHotelRepository,
                    HotelRepository>();
                builder.Services.AddScoped<IAuthService,
                    AuthService>();
                builder.Services.AddScoped<IHotelService,
                    HotelService>();
                builder.Services.AddScoped<TokenService>();

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAngular", policy =>
                    {
                        policy.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                });

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new
                        Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "Enter your JWT token here"
                    });

                    options.AddSecurityRequirement(new
                        Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            new string[] {}
        }
    });
                });

                var app = builder.Build();

                app.UseMiddleware<GlobalExceptionMiddleware>();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();
                app.UseCors("AllowAngular");
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                Log.Information(
                    "HotelAuthService started on port 5001");

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "HotelAuthService failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}