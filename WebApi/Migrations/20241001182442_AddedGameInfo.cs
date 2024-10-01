using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddedGameInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GameVersionInfoId",
                table: "LoomianSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "GameVersionInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GameEdition = table.Column<int>(type: "integer", nullable: false),
                    VersionCode = table.Column<string>(type: "text", nullable: false),
                    VersionTitle = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVersionInfos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_GameVersionInfoId",
                table: "LoomianSets",
                column: "GameVersionInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_GameVersionInfos_GameVersionInfoId",
                table: "LoomianSets",
                column: "GameVersionInfoId",
                principalTable: "GameVersionInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_GameVersionInfos_GameVersionInfoId",
                table: "LoomianSets");

            migrationBuilder.DropTable(
                name: "GameVersionInfos");

            migrationBuilder.DropIndex(
                name: "IX_LoomianSets_GameVersionInfoId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "GameVersionInfoId",
                table: "LoomianSets");
        }
    }
}
