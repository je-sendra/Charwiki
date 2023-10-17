using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VewTech.Charwiki.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "LoomianId",
                table: "Sets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sets",
                table: "Sets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LoomianLoomianAbility",
                columns: table => new
                {
                    LoomianAbilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoomianId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianLoomianAbility", x => new { x.LoomianAbilityId, x.LoomianId });
                    table.ForeignKey(
                        name: "FK_LoomianLoomianAbility_LoomianAbilities_LoomianAbilityId",
                        column: x => x.LoomianAbilityId,
                        principalTable: "LoomianAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoomianLoomianAbility_Loomians_LoomianId",
                        column: x => x.LoomianId,
                        principalTable: "Loomians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoveSet",
                columns: table => new
                {
                    MoveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveSet", x => new { x.MoveId, x.SetId });
                    table.ForeignKey(
                        name: "FK_MoveSet_Moves_MoveId",
                        column: x => x.MoveId,
                        principalTable: "Moves",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MoveSet_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoomianLoomianAbility_LoomianId",
                table: "LoomianLoomianAbility",
                column: "LoomianId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveSet_SetId",
                table: "MoveSet",
                column: "SetId");

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "LoomianLoomianAbility");

            migrationBuilder.DropTable(
                name: "MoveSet");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "LoomianId",
                table: "Set",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
