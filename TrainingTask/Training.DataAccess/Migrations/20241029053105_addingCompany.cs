using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addingCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    comid = table.Column<string>(type: "text", nullable: false),
                    comname = table.Column<string>(type: "text", nullable: false),
                    basic = table.Column<decimal>(type: "numeric", nullable: false),
                    hrent = table.Column<decimal>(type: "numeric", nullable: false),
                    medical = table.Column<decimal>(type: "numeric", nullable: false),
                    isinactive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_companies", x => x.comid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
