using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FARM_ATTENDANCE_SYSTEM.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "date", nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FPhoneNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IDNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GENDER = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FPO_COOP_Group = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AGE = table.Column<int>(type: "int", nullable: false),
                    VENUE = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FIELDCOORDINATOR = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    County = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsManaged = table.Column<bool>(type: "bit", nullable: false),
                    ListofFarmers = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.FarmerId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Position = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrCompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrSubCounty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrWard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrCountyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrCompanyNameId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrSubCountyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StrWardId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Position);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGroup = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UserGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateExpiry = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
