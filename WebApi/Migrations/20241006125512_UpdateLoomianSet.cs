using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLoomianSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_LoomianAbilities_AbilityId",
                table: "LoomianSets");

            migrationBuilder.RenameColumn(
                name: "AbilityId",
                table: "LoomianSets",
                newName: "LoomianAbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_LoomianSets_AbilityId",
                table: "LoomianSets",
                newName: "IX_LoomianSets_LoomianAbilityId");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "LoomianSets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_LoomianAbilities_LoomianAbilityId",
                table: "LoomianSets",
                column: "LoomianAbilityId",
                principalTable: "LoomianAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_LoomianAbilities_LoomianAbilityId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "LoomianSets");

            migrationBuilder.RenameColumn(
                name: "LoomianAbilityId",
                table: "LoomianSets",
                newName: "AbilityId");

            migrationBuilder.RenameIndex(
                name: "IX_LoomianSets_LoomianAbilityId",
                table: "LoomianSets",
                newName: "IX_LoomianSets_AbilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_LoomianAbilities_AbilityId",
                table: "LoomianSets",
                column: "AbilityId",
                principalTable: "LoomianAbilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
