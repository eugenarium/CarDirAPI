using CarDirAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDirAPI.Data
{
    public class CarDirDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public CarDirDb(DbContextOptions<CarDirDb> options) : base(options) { Database.EnsureCreated(); }
    }
}
