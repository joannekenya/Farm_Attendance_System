using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol.Plugins;
using Syncfusion.EJ2.PivotView;

namespace FARM_ATTENDANCE_SYSTEM.Models
{
    public class Farmers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FarmerId { get; set; }

        [Required]
        [StringLength(100)]
        public string FarmerName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Enter a valid 10-digit phone number starting with 0.")]
        public string FPhoneNo { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string IDNO { get; set; } = string.Empty;

        [Required]
        [StringLength(1)]
        public string GENDER { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string FPO_COOP_Group { get; set; } = string.Empty;

        [Required]
        public decimal AMOUNT { get; set; }

        [Required]
        public int AGE { get; set; }

        [Required]
        [StringLength(100)]
        public string VENUE { get; set; } = string.Empty;


        [Column("county_name")] // Maps to the database column "county_name"
        public required string County { get; set; }

        [Column("sub_counties")] // Maps to the database column "sub_counties"
        public required string SubCounties { get; set; }
        [Required]
        [StringLength(50)]
        public string FIELDCOORDINATOR { get; set; } = string.Empty;

        [StringLength(50)]
        public string? AuditBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public bool Audit { get; set; }
     
	
   


    }
}
