using System.ComponentModel.DataAnnotations;

namespace FARM_ATTENDANCE_SYSTEM.Models
{
    public class Locations
    {
        [Key]
        public int id { get; set; }

    
        public string county_name { get; set; } = string.Empty;

       
        public string sub_counties { get; set; } = string.Empty;


       

    }
}
