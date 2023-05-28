using Microsoft.EntityFrameworkCore.Migrations;

namespace Lec11.Migrations
{
    public partial class AddedNewGallaryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksGallary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksGallary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksGallary_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksGallary_BookId",
                table: "BooksGallary",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksGallary");
        }
    }
}
