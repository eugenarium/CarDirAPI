using System.ComponentModel.DataAnnotations;

namespace CarDirAPI.Models
{
    public class Car
    {
        [Key]
        public string Number { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime DateAdded { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Car))
            {
                return false;
            }

            var other = (Car)obj;

            return Number.Equals(other.Number);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}
