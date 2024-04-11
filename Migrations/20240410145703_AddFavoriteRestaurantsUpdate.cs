using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    public partial class AddFavoriteRestaurantsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesRestaurants",
                table: "FavoritesRestaurants");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "FavoritesRestaurants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoritesRestaurants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesRestaurants",
                table: "FavoritesRestaurants",
                columns: new[] { "RestaurantId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_FavoritesRestaurants_UserId",
                table: "FavoritesRestaurants",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesRestaurants_AspNetUsers_UserId",
                table: "FavoritesRestaurants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesRestaurants_Restaurants_RestaurantId",
                table: "FavoritesRestaurants",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesRestaurants_AspNetUsers_UserId",
                table: "FavoritesRestaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesRestaurants_Restaurants_RestaurantId",
                table: "FavoritesRestaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesRestaurants",
                table: "FavoritesRestaurants");

            migrationBuilder.DropIndex(
                name: "IX_FavoritesRestaurants_UserId",
                table: "FavoritesRestaurants");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoritesRestaurants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "FavoritesRestaurants",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesRestaurants",
                table: "FavoritesRestaurants",
                column: "Key");
        }
    }
}
