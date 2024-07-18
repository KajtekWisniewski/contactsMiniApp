
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
            
            //utworzenie buildera aplikacji

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddScoped<IContactRepository, ContactRepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

            //dodanie do aplikacji inicjalnego adresu identityprovidera
            builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration,
                    option =>
                    {
                        option.TokenValidationParameters.ValidIssuer = "http://localhost:8080/realms/myrealm";
                    }
                );

            //serwisy zwiazanie z autoryzacja keycloak
            builder.Services
                .AddAuthorization()
                .AddKeycloakAuthorization(builder.Configuration)
                .AddAuthorizationBuilder()
                .AddPolicy("require admin role", policy => policy.RequireResourceRoles(["admin"]));


            builder.Services.AddCors();

            //dodanie kontekstu bazy danych, polaczenie z postgre za pomoca biblioteki

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
            });
            
            Console.WriteLine(builder.Configuration.GetConnectionString("Postgres"));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {

            }
            app.UseCors(options =>
                options
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000"));
            
            //migracja do bazy danych
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
