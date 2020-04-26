using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreCQRSdemo.Persistence.Migrations
{
    public partial class EntityType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "RecipeExcerpt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "Cocktail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityType",
                table: "AppEvent",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "RecipeExcerpt");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "Cocktail");

            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "AppEvent");
        }
    }
}
