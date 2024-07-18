
using Microsoft.EntityFrameworkCore;
using ContactsMicroservice.Configuration;
using ContactsMicroservice.Data;
using ContactsMicroservice.DTOs;
using ContactsMicroservice.Repository;
using ContactsMicroservice.Repository.Contracts;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;


namespace ContactsMicroservice
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IContactRepository, ContactRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration,
                    option =>
                    {
                        option.TokenValidationParameters.ValidIssuer = "http://localhost:8080/realms/myrealm";
                    }
                );

            builder.Services
                .AddAuthorization()
                .AddKeycloakAuthorization(builder.Configuration)
                .AddAuthorizationBuilder()
                .AddPolicy("require admin role", policy => policy.RequireResourceRoles(["admin"]));

            builder.Services.AddCors();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

            }
            app.UseCors(options =>
                options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000"));

            using var scope = app.Services.CreateScope();
			var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			if ((await db.Database.GetPendingMigrationsAsync()).Any()) await db.Database.MigrateAsync();

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
