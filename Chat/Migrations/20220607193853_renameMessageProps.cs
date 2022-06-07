using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Migrations
{
    public partial class renameMessageProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Text",
                table: "messages",
                newName: "text");

            migrationBuilder.RenameColumn(
                name: "_NameOfUser",
                table: "messages",
                newName: "userName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "messages",
                newName: "_NameOfUser");

            migrationBuilder.RenameColumn(
                name: "text",
                table: "messages",
                newName: "_Text");
        }
    }
}
