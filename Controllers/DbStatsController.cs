using CarDirAPI.Models;
using CarDirAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarDirAPI.Controllers
{
    [ApiController]
    [Route("api/db_stats")]
    public class DbStatsController : ControllerBase
    {
        private readonly ICarRepository _carRepository;
        public DbStatsController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        [HttpGet]
        public async Task<ActionResult<DbStat>> GetStats()
        {
            var stats = await _carRepository.GetDbStats();

            return stats == null ? NotFound("Таблица пуста, статистики нет.") : Ok(stats);
        }
    }
}
