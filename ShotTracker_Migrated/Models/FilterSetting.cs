using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShotTracker.Models
{
    public class FilterSetting
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Value { get; set; }
    }
}
