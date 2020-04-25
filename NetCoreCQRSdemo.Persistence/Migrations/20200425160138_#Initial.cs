using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCoreCQRSdemo.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEvent",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Arguments = table.Column<string>(nullable: true),
                    CommandCode = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Strength = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipeExcerpt",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CocktailId = table.Column<string>(nullable: true),
                    IngredientId = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeExcerpt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipeExcerpt_Cocktail_CocktailId",
                        column: x => x.CocktailId,
                        principalTable: "Cocktail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RecipeExcerpt_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppEvent_Id",
                table: "AppEvent",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cocktail_Id",
                table: "Cocktail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_Id",
                table: "Ingredient",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeExcerpt_CocktailId",
                table: "RecipeExcerpt",
                column: "CocktailId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeExcerpt_Id",
                table: "RecipeExcerpt",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeExcerpt_IngredientId",
                table: "RecipeExcerpt",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEvent");

            migrationBuilder.DropTable(
                name: "RecipeExcerpt");

            migrationBuilder.DropTable(
                name: "Cocktail");

            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
