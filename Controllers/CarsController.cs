using CarDirAPI.Models;
using CarDirAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDirAPI.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;

        public CarsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetCars([FromQuery] string? brand, [FromQuery] string? color, [FromQuery] int? year)
        {
            var cars = await _carRepository.GetCars(brand, color, year);
            return cars.Count == 0 ? NotFound() : Ok(cars);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCar([FromQuery] string number)
        {
            var deleted = await _carRepository.DeleteCar(number);
            return deleted ? NoContent() : NotFound("Автомобиля с таким номером не существует.");
        }

        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCar(CarDto car)
        {
            if (await _carRepository.CheckIfCarAlreadyExists(car.Number)) return BadRequest("Автомобиль с таким номером уже существует!");
            if (!car.CorrectNumber()) return UnprocessableEntity("Номер автомобиля не соответствует шаблону!");
            if (car.ReleaseYear < 1900 || car.ReleaseYear > DateTime.Now.Year) 
                return UnprocessableEntity("Год производства не может быть меньше 1900 или больше текущего года!");
            var created = await _carRepository.CreateCar(car);
            return created ? CreatedAtAction(nameof(GetCars), car) : BadRequest("Неверно введены данные!");
        }
    }
}
