using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotTracker.Models
{
    public class UserData
    {
        [PrimaryKey][AutoIncrement]
        public int ID { get; set; }
        public bool ReviewShown { get; set; } = false;
    }
}
