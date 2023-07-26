using CarDirAPI.Models;

namespace CarDirAPI.Services
{
    public interface ICarRepository
    {
        //методы для работы с машинами
        public Task<List<CarDto>> GetCars(string? brand, string? color, int? year);
        public Task<bool> CreateCar(CarDto car);
        public Task<bool> DeleteCar(string number);
        public Task<bool> CheckIfCarAlreadyExists(string number);

        //метод для работы со статистикой бд
        public Task<DbStat?> GetDbStats();

    }
}
