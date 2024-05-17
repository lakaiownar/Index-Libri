using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Index_Libri.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookGoogleIdProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleId",
                table: "Book",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleId",
                table: "Book");
        }
    }
}
