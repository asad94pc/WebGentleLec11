using Microsoft.EntityFrameworkCore.Migrations;

namespace Lec11.Migrations
{
    public partial class AddedImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Books");
        }
    }
}
