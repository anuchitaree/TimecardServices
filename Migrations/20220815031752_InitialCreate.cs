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
                name: "MpCalculateTimecardRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpId = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Direction = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    MachineSn = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MpCalculateTimecardRecords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MpCalculateTimecardRecords");
        }
    }
}
