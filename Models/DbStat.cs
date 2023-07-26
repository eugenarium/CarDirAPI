namespace CarDirAPI.Models
{
    public class DbStat
    {
        public int Count { get; set; }
        public DateTime First { get; set; }
        public DateTime Last { get; set; }

        public DbStat(int count, DateTime first, DateTime last)
        {
            Count = count;
            First = first;
            Last = last;
        }
    }
}
