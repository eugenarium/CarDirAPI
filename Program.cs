using CarDirAPI.Data;
using CarDirAPI.Data.SeedData;
using CarDirAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CarDirAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<CarDirDb>(opt =>
                opt.UseSqlite(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ??
                    throw new InvalidOperationException("Connection string not found.")));
            builder.Services.AddScoped<ICarRepository, CarRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                Seed.Initialize(services);
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}