using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addingShiftDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "shifts",
                table: "companies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    shiftname = table.Column<string>(type: "text", nullable: false),
                    @in = table.Column<TimeSpan>(name: "in", type: "interval", nullable: false),
                    @out = table.Column<TimeSpan>(name: "out", type: "interval", nullable: false),
                    late = table.Column<TimeSpan>(type: "interval", nullable: false),
                    comid = table.Column<string>(type: "text", nullable: false),
                    isdeleted = table.Column<bool>(type: "boolean", nullable: false),
                    upby = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shifts", x => x.id);
                    table.ForeignKey(
                        name: "fk_shifts_companies_comid",
                        column: x => x.comid,
                        principalTable: "companies",
                        principalColumn: "comid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_shifts_comid",
                table: "shifts",
                column: "comid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropColumn(
                name: "shifts",
                table: "companies");
        }
    }
}
