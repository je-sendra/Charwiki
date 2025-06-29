using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToLoomianSetStarRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserToLoomianSetStarRating",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoomianSetId = table.Column<Guid>(type: "uuid", nullable: false),
                    StarRating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToLoomianSetStarRating", x => new { x.UserId, x.LoomianSetId });
                    table.ForeignKey(
                        name: "FK_UserToLoomianSetStarRating_LoomianSets_LoomianSetId",
                        column: x => x.LoomianSetId,
                        principalTable: "LoomianSets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserToLoomianSetStarRating_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loomians_Name",
                table: "Loomians",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoomianMoves_Name",
                table: "LoomianMoves",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoomianItems_Name",
                table: "LoomianItems",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoomianAbilities_Name",
                table: "LoomianAbilities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserToLoomianSetStarRating_LoomianSetId",
                table: "UserToLoomianSetStarRating",
                column: "LoomianSetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserToLoomianSetStarRating");

            migrationBuilder.DropIndex(
                name: "IX_Loomians_Name",
                table: "Loomians");

            migrationBuilder.DropIndex(
                name: "IX_LoomianMoves_Name",
                table: "LoomianMoves");

            migrationBuilder.DropIndex(
                name: "IX_LoomianItems_Name",
                table: "LoomianItems");

            migrationBuilder.DropIndex(
                name: "IX_LoomianAbilities_Name",
                table: "LoomianAbilities");
        }
    }
}
