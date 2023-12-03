using ShotTracker.Enums;
using SQLite;
using System;

namespace ShotTracker.Models
{
    public class ShotEntry
    {
        [PrimaryKey][AutoIncrement]
        public int ID { get; set; }
        public int Makes { get; set; }
        public int Misses { get; set; }
        public string TextResult
        {
            get
            {
                return $"{Makes}/{Makes + Misses}";
            }
        }
        public string TextCourtType
        {
            get
            {
                return $"{CourtType.GetDescription()}";
            }
        }
        public ShotLocation Location { get; set; }
        public CourtType CourtType { get; set; }
        public DateTime Date {get; set;}
    }
}