using CarDirAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CarDirAPI.Data.SeedData
{
    public class Seed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarDirDb(serviceProvider.GetRequiredService<DbContextOptions<CarDirDb>>()))
            {
                if (context == null || context.Cars == null)
                {
                    throw new ArgumentNullException("Null CarDirDb");
                }

                // Look for any movies.
                if (context.Cars.Any())
                {
                    return;   // DB has been seeded
                }

                var carList = new List<Car>();
                for (int i = 0; i < 100; i++)
                {
                    var car = new Car
                    {
                        Number = RandomizeNumber(),
                        Brand = RandomizeBrand(),
                        Color = RandomizeColor(),
                        ReleaseYear = RandomizeYear(),
                        DateAdded = DateTime.Now,
                    };
                    carList.Add(car);
                }

                context.Cars.AddRange(carList);
                context.SaveChanges();
            }
        }

        private static string RandomizeNumber()
        {
            var random = new Random();
            var sb = new StringBuilder();

            for (int i = 0; i < 2; i++)
                sb.Append((char)random.Next('А', 'Я' + 1));

            for (int i = 0; i < 3; i++)
                sb.Append(random.Next(0, 10));

            sb.Append((char)random.Next('А', 'Я' + 1));

            sb.Append(random.Next(0, 2) == 1 ? "196" : "96");

            return sb.ToString();

        }

        private static string RandomizeBrand()
        {
            var random = new Random();
            var brands = new List<string>
            {
                //"bmw",
                //"audi",
                //"mercedes",
                //"lada",
                //"mini",
                //"smart",
                //"suzuki",
                //"peugeot",
                //"opel",
                //"jeep",
                //"honda",
                //"ford",
                "kia",
                "hyundai",
                "renault",
                "nissan",
                "toyota",
                "volkswagen",
                "skoda"
            };
            return brands[random.Next(0, brands.Count)];
        }
        private static string RandomizeColor()
        {
            var random = new Random();
            var colors = new List<string>
            {
                "white",
                "black",
                "blue",
                "red",
                "green",
                "yellow"
            };
            return colors[random.Next(0, colors.Count)];
        }

        private static int RandomizeYear()
        {
            var random = new Random();
            return random.Next(1990, 2023 + 1);
        }
    }
}
