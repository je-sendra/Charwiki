using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceValueToStatAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId1",
                table: "ValueToStatAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId2",
                table: "ValueToStatAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId1",
                table: "ValueToStatAssignments");

            migrationBuilder.DropIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId2",
                table: "ValueToStatAssignments");

            migrationBuilder.DropColumn(
                name: "LoomianSetId1",
                table: "ValueToStatAssignments");

            migrationBuilder.DropColumn(
                name: "LoomianSetId2",
                table: "ValueToStatAssignments");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingPointsId",
                table: "LoomianSets",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UniquePointsId",
                table: "LoomianSets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StatsSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Energy = table.Column<int>(type: "integer", nullable: false),
                    MeleeAttack = table.Column<int>(type: "integer", nullable: false),
                    RangedAttack = table.Column<int>(type: "integer", nullable: false),
                    MeleeDefense = table.Column<int>(type: "integer", nullable: false),
                    RangedDefense = table.Column<int>(type: "integer", nullable: false),
                    Speed = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsSets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_TrainingPointsId",
                table: "LoomianSets",
                column: "TrainingPointsId");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_UniquePointsId",
                table: "LoomianSets",
                column: "UniquePointsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_StatsSets_TrainingPointsId",
                table: "LoomianSets",
                column: "TrainingPointsId",
                principalTable: "StatsSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_StatsSets_UniquePointsId",
                table: "LoomianSets",
                column: "UniquePointsId",
                principalTable: "StatsSets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_StatsSets_TrainingPointsId",
                table: "LoomianSets");

            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_StatsSets_UniquePointsId",
                table: "LoomianSets");

            migrationBuilder.DropTable(
                name: "StatsSets");

            migrationBuilder.DropIndex(
                name: "IX_LoomianSets_TrainingPointsId",
                table: "LoomianSets");

            migrationBuilder.DropIndex(
                name: "IX_LoomianSets_UniquePointsId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "TrainingPointsId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "UniquePointsId",
                table: "LoomianSets");

            migrationBuilder.AddColumn<Guid>(
                name: "LoomianSetId1",
                table: "ValueToStatAssignments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LoomianSetId2",
                table: "ValueToStatAssignments",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId1",
                table: "ValueToStatAssignments",
                column: "LoomianSetId1");

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId2",
                table: "ValueToStatAssignments",
                column: "LoomianSetId2");

            migrationBuilder.AddForeignKey(
                name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId1",
                table: "ValueToStatAssignments",
                column: "LoomianSetId1",
                principalTable: "LoomianSets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId2",
                table: "ValueToStatAssignments",
                column: "LoomianSetId2",
                principalTable: "LoomianSets",
                principalColumn: "Id");
        }
    }
}
