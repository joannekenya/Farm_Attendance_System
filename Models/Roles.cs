using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace FARM_ATTENDANCE_SYSTEM.Models
{
    public class Roles 
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public required string UserGroup { get; set; }
    }
}
