using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToLoomianSetRatingsRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToLoomianSetStarRating_LoomianSets_LoomianSetId",
                table: "UserToLoomianSetStarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToLoomianSetStarRating_Users_UserId",
                table: "UserToLoomianSetStarRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToLoomianSetStarRating",
                table: "UserToLoomianSetStarRating");

            migrationBuilder.RenameTable(
                name: "UserToLoomianSetStarRating",
                newName: "UserToLoomianSetStarRatings");

            migrationBuilder.RenameIndex(
                name: "IX_UserToLoomianSetStarRating_LoomianSetId",
                table: "UserToLoomianSetStarRatings",
                newName: "IX_UserToLoomianSetStarRatings_LoomianSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToLoomianSetStarRatings",
                table: "UserToLoomianSetStarRatings",
                columns: new[] { "UserId", "LoomianSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserToLoomianSetStarRatings_LoomianSets_LoomianSetId",
                table: "UserToLoomianSetStarRatings",
                column: "LoomianSetId",
                principalTable: "LoomianSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToLoomianSetStarRatings_Users_UserId",
                table: "UserToLoomianSetStarRatings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserToLoomianSetStarRatings_LoomianSets_LoomianSetId",
                table: "UserToLoomianSetStarRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToLoomianSetStarRatings_Users_UserId",
                table: "UserToLoomianSetStarRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserToLoomianSetStarRatings",
                table: "UserToLoomianSetStarRatings");

            migrationBuilder.RenameTable(
                name: "UserToLoomianSetStarRatings",
                newName: "UserToLoomianSetStarRating");

            migrationBuilder.RenameIndex(
                name: "IX_UserToLoomianSetStarRatings_LoomianSetId",
                table: "UserToLoomianSetStarRating",
                newName: "IX_UserToLoomianSetStarRating_LoomianSetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserToLoomianSetStarRating",
                table: "UserToLoomianSetStarRating",
                columns: new[] { "UserId", "LoomianSetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserToLoomianSetStarRating_LoomianSets_LoomianSetId",
                table: "UserToLoomianSetStarRating",
                column: "LoomianSetId",
                principalTable: "LoomianSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToLoomianSetStarRating_Users_UserId",
                table: "UserToLoomianSetStarRating",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
