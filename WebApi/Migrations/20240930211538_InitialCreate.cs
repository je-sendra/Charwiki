using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoomianAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoomianItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoomianMoves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianMoves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Loomians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type1 = table.Column<int>(type: "integer", nullable: false),
                    Type2 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loomians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoomianSets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LoomianId = table.Column<Guid>(type: "uuid", nullable: false),
                    AbilityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    Move1Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Move2Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Move3Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Move4Id = table.Column<Guid>(type: "uuid", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Explanation = table.Column<string>(type: "text", nullable: true),
                    Strategy = table.Column<string>(type: "text", nullable: true),
                    Strengths = table.Column<List<string>>(type: "text[]", nullable: true),
                    Weaknesses = table.Column<List<string>>(type: "text[]", nullable: true),
                    OtherOptions = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianAbilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "LoomianAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "LoomianItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianMoves_Move1Id",
                        column: x => x.Move1Id,
                        principalTable: "LoomianMoves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianMoves_Move2Id",
                        column: x => x.Move2Id,
                        principalTable: "LoomianMoves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianMoves_Move3Id",
                        column: x => x.Move3Id,
                        principalTable: "LoomianMoves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoomianSets_LoomianMoves_Move4Id",
                        column: x => x.Move4Id,
                        principalTable: "LoomianMoves",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LoomianSets_Loomians_LoomianId",
                        column: x => x.LoomianId,
                        principalTable: "Loomians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValueToStatAssignments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    Stat = table.Column<int>(type: "integer", nullable: false),
                    LoomianSetId = table.Column<Guid>(type: "uuid", nullable: true),
                    LoomianSetId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    LoomianSetId2 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueToStatAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId",
                        column: x => x.LoomianSetId,
                        principalTable: "LoomianSets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId1",
                        column: x => x.LoomianSetId1,
                        principalTable: "LoomianSets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ValueToStatAssignments_LoomianSets_LoomianSetId2",
                        column: x => x.LoomianSetId2,
                        principalTable: "LoomianSets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_AbilityId",
                table: "LoomianSets",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_ItemId",
                table: "LoomianSets",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_LoomianId",
                table: "LoomianSets",
                column: "LoomianId");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_Move1Id",
                table: "LoomianSets",
                column: "Move1Id");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_Move2Id",
                table: "LoomianSets",
                column: "Move2Id");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_Move3Id",
                table: "LoomianSets",
                column: "Move3Id");

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_Move4Id",
                table: "LoomianSets",
                column: "Move4Id");

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId",
                table: "ValueToStatAssignments",
                column: "LoomianSetId");

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId1",
                table: "ValueToStatAssignments",
                column: "LoomianSetId1");

            migrationBuilder.CreateIndex(
                name: "IX_ValueToStatAssignments_LoomianSetId2",
                table: "ValueToStatAssignments",
                column: "LoomianSetId2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValueToStatAssignments");

            migrationBuilder.DropTable(
                name: "LoomianSets");

            migrationBuilder.DropTable(
                name: "LoomianAbilities");

            migrationBuilder.DropTable(
                name: "LoomianItems");

            migrationBuilder.DropTable(
                name: "LoomianMoves");

            migrationBuilder.DropTable(
                name: "Loomians");
        }
    }
}
