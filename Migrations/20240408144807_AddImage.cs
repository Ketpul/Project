using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Restaurants",
                newName: "ImageUrl3");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl1",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl2",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl1",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "ImageUrl2",
                table: "Restaurants");

            migrationBuilder.RenameColumn(
                name: "ImageUrl3",
                table: "Restaurants",
                newName: "ImageUrl");
        }
    }
}
