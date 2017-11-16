using System;
using SQLite;

namespace Gym.Models
{
    public class DailyEntry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public DateTime Day { get; set; }
        public int Count { get; set; }
    }
}
