using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Index_Libri.Server.Migrations
{
    /// <inheritdoc />
    public partial class IndexLibri2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser_BookList_BookListId",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_BookListId",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "BookListId",
                table: "ApplicationUser");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "BookList",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "BookList",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookListId",
                table: "Book",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookList_ApplicationUserId",
                table: "BookList",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookListId",
                table: "Book",
                column: "BookListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookList_BookListId",
                table: "Book",
                column: "BookListId",
                principalTable: "BookList",
                principalColumn: "BookListId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookList_ApplicationUser_ApplicationUserId",
                table: "BookList",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "dbId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookList_BookListId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_BookList_ApplicationUser_ApplicationUserId",
                table: "BookList");

            migrationBuilder.DropIndex(
                name: "IX_BookList_ApplicationUserId",
                table: "BookList");

            migrationBuilder.DropIndex(
                name: "IX_Book_BookListId",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "BookList");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "BookList");

            migrationBuilder.DropColumn(
                name: "BookListId",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "BookListId",
                table: "ApplicationUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_BookListId",
                table: "ApplicationUser",
                column: "BookListId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_BookList_BookListId",
                table: "ApplicationUser",
                column: "BookListId",
                principalTable: "BookList",
                principalColumn: "BookListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
