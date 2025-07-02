using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UseStatsSetForPersonality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValueToStatAssignments");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonalityModifiersId",
                table: "LoomianSets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_PersonalityModifiersId",
                table: "LoomianSets",
                column: "PersonalityModifiersId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_StatsSets_PersonalityModifiersId",
                table: "LoomianSets",
                column: "PersonalityModifiersId",
                principalTable: "StatsSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_StatsSets_PersonalityModifiersId",
                table: "LoomianSets");

            migrationBuilder.DropIndex(
                name: "IX_LoomianSets_PersonalityModifiersId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "PersonalityModifiersId",
                table: "LoomianSets");

            migrationBuilder.CreateTable(
                name: "ValueToStatAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoomianSetId = table.Column<Guid>(type: "uuid", nullable: true),
                    Stat = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueToStatAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId",
                        column: x => x.LoomianSetId,
                        principalTable: "LoomianSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId",
                table: "ValueToStatAssignments",
                column: "LoomianSetId");
        }
    }
}
