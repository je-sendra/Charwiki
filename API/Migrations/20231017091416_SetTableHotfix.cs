using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VewTech.Charwiki.API.Migrations
{
    /// <inheritdoc />
    public partial class SetTableHotfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Sets_SetId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_LoomianAbilities_AbilityId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Loomians_LoomianId",
                table: "Sets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sets",
                table: "Sets");

            migrationBuilder.RenameTable(
                name: "Sets",
                newName: "Set");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_LoomianId",
                table: "Set",
                newName: "IX_Set_LoomianId");

            migrationBuilder.RenameIndex(
                name: "IX_Sets_AbilityId",
                table: "Set",
                newName: "IX_Set_AbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Set",
                table: "Set",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Set_SetId",
                table: "Moves",
                column: "SetId",
                principalTable: "Set",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Set_LoomianAbilities_AbilityId",
                table: "Set",
                column: "AbilityId",
                principalTable: "LoomianAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Set_Loomians_LoomianId",
                table: "Set",
                column: "LoomianId",
                principalTable: "Loomians",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Set_SetId",
                table: "Moves");

            migrationBuilder.DropForeignKey(
                name: "FK_Set_LoomianAbilities_AbilityId",
                table: "Set");

            migrationBuilder.DropForeignKey(
                name: "FK_Set_Loomians_LoomianId",
                table: "Set");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Set",
                table: "Set");

            migrationBuilder.RenameTable(
                name: "Set",
                newName: "Sets");

            migrationBuilder.RenameIndex(
                name: "IX_Set_LoomianId",
                table: "Sets",
                newName: "IX_Sets_LoomianId");

            migrationBuilder.RenameIndex(
                name: "IX_Set_AbilityId",
                table: "Sets",
                newName: "IX_Sets_AbilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sets",
                table: "Sets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Sets_SetId",
                table: "Moves",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_LoomianAbilities_AbilityId",
                table: "Sets",
                column: "AbilityId",
                principalTable: "LoomianAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Loomians_LoomianId",
                table: "Sets",
                column: "LoomianId",
                principalTable: "Loomians",
                principalColumn: "Id");
        }
    }
}
