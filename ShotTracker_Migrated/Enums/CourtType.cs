using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShotTracker.Enums
{
    public enum CourtType
    {
        [Description("Unspecified")]
        Unspecified = 0,
        [Description("Indoor")]
        Indoor = 1,
        [Description("Outdoor")]
        Outdoor = 2
    }
}
