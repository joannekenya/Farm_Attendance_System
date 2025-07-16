using Microsoft.EntityFrameworkCore;
using FARM_ATTENDANCE_SYSTEM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace FARM_ATTENDANCE_SYSTEM.Data
{
    public class FarmDbContext :DbContext
    {
        public FarmDbContext(DbContextOptions<FarmDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Farmers> Farmers { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Locations> Locations { get; set; }
    }
}
