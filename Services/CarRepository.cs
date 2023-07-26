using CarDirAPI.Data;
using CarDirAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDirAPI.Services
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDirDb _db;

        public CarRepository(CarDirDb db)
        {
            _db = db;
        }

        public async Task<bool> CheckIfCarAlreadyExists(string number)
        {
            return await _db.Cars.FindAsync(number) != null;
        }

        public async Task<bool> CreateCar(CarDto car)
        {

            Car carToAdd = new Car
            {
                Number = car.Number,
                Brand = car.Brand.ToLower(),
                Color = car.Color.ToLower(),
                ReleaseYear = car.ReleaseYear,
                DateAdded = DateTime.Now
            };

            if (_db.Cars.Contains(carToAdd)) return false;

            _db.Cars.Add(carToAdd);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCar(string number)
        {
            var car = await _db.Cars.FindAsync(number);
            if (car == null) return false;
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<List<CarDto>> GetCars(string? brand, string? color, int? year)
        {
            var cars = await _db.Cars
                .Where(car =>
                    (string.IsNullOrEmpty(brand) || car.Brand.Equals(brand))
                    &&
                    (string.IsNullOrEmpty(color) || car.Color.Equals(color))
                    &&
                    (!year.HasValue || car.ReleaseYear.Equals(year)))
                .Select(car => new CarDto(car))
                .ToListAsync();
            return cars;
        }

        public async Task<DbStat?> GetDbStats()
        {
            var total = await _db.Cars.CountAsync();
            if (total == 0) return null;

            var first = await _db.Cars.OrderBy(car => car.DateAdded).Select(car => car.DateAdded).FirstOrDefaultAsync();
            var last = await _db.Cars.OrderByDescending(car => car.DateAdded).Select(car => car.DateAdded).FirstOrDefaultAsync();

            return new DbStat(total, first, last);
        }
    }
}
