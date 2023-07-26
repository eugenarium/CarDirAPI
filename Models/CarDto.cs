using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace CarDirAPI.Models
{
    public class CarDto
    {
        [JsonRequired]
        public string Number { get; set; }

        [JsonRequired]
        public string Brand { get; set; }

        [JsonRequired]
        public string Color { get; set; }

        [JsonRequired]
        [JsonPropertyName("year")]
        public int ReleaseYear { get; set; }

        //конструктор без параметров нужен, чтобы EF принимал новый объект json postом
        //альтернативой может быть метод в репозитории, который будет принимать Car и возвращать CarDto
        public CarDto() { }

        public CarDto(Car car)
        {
            Number = car.Number;
            Brand = car.Brand;
            Color = car.Color;
            ReleaseYear = car.ReleaseYear;
        }

        // Проверяем, соответствует ли введенный номер автомобиля шаблону номера АА999А96.
        // Только UPPER.
        public bool CorrectNumber()
        {
            var pattern = "^[А-Я]{2}\\d{3}[А-Я]\\d{2,3}$";
            return Regex.IsMatch(Number, pattern);
        }
    }
}
