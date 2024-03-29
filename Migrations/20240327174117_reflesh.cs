using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class reflesh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Owners_OwnerUserId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OwnerUserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Owners");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Owners_OwnerId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_OwnerId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "OwnerUserId",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Owners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_OwnerUserId",
                table: "Restaurants",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Owners_OwnerUserId",
                table: "Restaurants",
                column: "OwnerUserId",
                principalTable: "Owners",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
