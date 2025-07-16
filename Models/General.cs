using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FARM_ATTENDANCE_SYSTEM.Models
{
    public class General
    {
        public List<Farmers> Farmers { get; set; } = new List<Farmers>();
        public List<Locations> Locations { get; set; } = new List<Locations>();

        // Optional: Constructor that initializes lists
        public General()
        {
            Farmers = new List<Farmers>();
            Locations = new List<Locations>();
        }
    }
}
