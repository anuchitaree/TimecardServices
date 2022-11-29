using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimecardServices.Migrations.TimeCard
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Registdatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimecardRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpId = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    MachineSn = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimecardRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogRecords");

            migrationBuilder.DropTable(
                name: "TimecardRecords");
        }
    }
}
