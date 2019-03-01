using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class ListItemDbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_Recipes_RecipeId",
                table: "ListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItem_ShopLists_ShopListId",
                table: "ListItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListItem",
                table: "ListItem");

            migrationBuilder.RenameTable(
                name: "ListItem",
                newName: "ListItems");

            migrationBuilder.RenameIndex(
                name: "IX_ListItem_ShopListId",
                table: "ListItems",
                newName: "IX_ListItems_ShopListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListItems",
                table: "ListItems",
                columns: new[] { "RecipeId", "ShopListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_Recipes_RecipeId",
                table: "ListItems",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItems_ShopLists_ShopListId",
                table: "ListItems",
                column: "ShopListId",
                principalTable: "ShopLists",
                principalColumn: "ShopListId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_Recipes_RecipeId",
                table: "ListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ListItems_ShopLists_ShopListId",
                table: "ListItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListItems",
                table: "ListItems");

            migrationBuilder.RenameTable(
                name: "ListItems",
                newName: "ListItem");

            migrationBuilder.RenameIndex(
                name: "IX_ListItems_ShopListId",
                table: "ListItem",
                newName: "IX_ListItem_ShopListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListItem",
                table: "ListItem",
                columns: new[] { "RecipeId", "ShopListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_Recipes_RecipeId",
                table: "ListItem",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ListItem_ShopLists_ShopListId",
                table: "ListItem",
                column: "ShopListId",
                principalTable: "ShopLists",
                principalColumn: "ShopListId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
