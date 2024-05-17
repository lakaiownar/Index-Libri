using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Index_Libri.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookStatusProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Book");
        }
    }
}
