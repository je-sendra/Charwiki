using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLoomianBaseStats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BaseStatsId",
                table: "Loomians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TagToLoomianSet_TagId",
                table: "TagToLoomianSet",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Loomians_BaseStatsId",
                table: "Loomians",
                column: "BaseStatsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loomians_StatsSets_BaseStatsId",
                table: "Loomians",
                column: "BaseStatsId",
                principalTable: "StatsSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagToLoomianSet_Tags_TagId",
                table: "TagToLoomianSet",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loomians_StatsSets_BaseStatsId",
                table: "Loomians");

            migrationBuilder.DropForeignKey(
                name: "FK_TagToLoomianSet_Tags_TagId",
                table: "TagToLoomianSet");

            migrationBuilder.DropIndex(
                name: "IX_TagToLoomianSet_TagId",
                table: "TagToLoomianSet");

            migrationBuilder.DropIndex(
                name: "IX_Loomians_BaseStatsId",
                table: "Loomians");

            migrationBuilder.DropColumn(
                name: "BaseStatsId",
                table: "Loomians");
        }
    }
}
