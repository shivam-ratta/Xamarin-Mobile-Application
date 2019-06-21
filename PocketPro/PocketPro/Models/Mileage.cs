using SQLite;
using System;

namespace PocketPro.Models
{
    public class Mileage
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public decimal Miles { get; set; }
    }
}