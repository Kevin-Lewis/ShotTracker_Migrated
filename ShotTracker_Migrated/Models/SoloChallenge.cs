using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotTracker_Migrated.Models
{
    public class SoloChallenge
    {
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }
    }
}
