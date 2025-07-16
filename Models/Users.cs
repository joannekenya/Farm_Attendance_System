using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FARM_ATTENDANCE_SYSTEM.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string UserGroup { get; set; } = string.Empty;

        [Required]
        [Phone]
        [MaxLength(50)]
        public string PhoneNo { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
        
        public bool EmailConfirmed { get; set; } = false;


        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password
        [NotMapped]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateExpiry { get; set; }
    }
}
