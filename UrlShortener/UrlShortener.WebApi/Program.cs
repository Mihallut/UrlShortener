using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using UrlShortener.Application;
using UrlShortener.Domain.Interfaces.Auth;
using UrlShortener.Domain.Interfaces.Repositories;
using UrlShortener.Domain.Interfaces.Services;
using UrlShortener.Infrastructure;
using UrlShortener.Infrastructure.Auth;
using UrlShortener.Infrastructure.Repositories;
using UrlShortener.Infrastructure.Services;
using UrlShortener.WebApi.Middlewares;


namespace UrlShortener.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // add JWT Authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "AuthorizationJwt",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = "AuthorizationJwt",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });
            });

            var configuration = builder.Configuration;

            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        string authorization = context.Request.Headers["AuthorizationJwt"];

                        // If no authorization header found, nothing to process further
                        if (string.IsNullOrEmpty(authorization))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }

                        if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = authorization.Substring("Bearer ".Length).Trim();
                        }

                        // If no token found, no further work possible
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            context.NoResult();
                            return Task.CompletedTask;
                        }

                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"])
                    ),
                    ClockSkew = TimeSpan.Zero
                };
            });

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApiDbContext>(options =>
                    options.UseNpgsql(connectionString));
            builder.Services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));

            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUrlRepository, UrlRepository>();

            builder.Services.AddScoped<IUrlShorteningService, UrlShorteningService>();

            builder.Services.AddHttpContextAccessor();


            // MediatR
            builder.Services.AddMediatR(c =>
                c.RegisterServicesFromAssemblyContaining<Program>());

            builder.Services.AddApplication();

            builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyMethod();
                options.AllowAnyOrigin();
            });
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
