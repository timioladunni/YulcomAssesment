using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YulcomAssesment.API.Migrations
{
    /// <inheritdoc />
    public partial class addingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApiUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndpointCalled = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YulcomAssesmentData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Defense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpAttack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpDefense = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Generation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legendary = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YulcomAssesmentData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "YulcomAssesmentData");
        }
    }
}
