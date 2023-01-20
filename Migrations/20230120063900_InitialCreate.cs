using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimecardServices.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    Registdatetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Result = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    Decription = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimecardRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    EmpId = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ChildLineId = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    MachineSn = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false)
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
