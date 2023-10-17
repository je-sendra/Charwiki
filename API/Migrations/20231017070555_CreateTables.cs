using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VewTech.Charwiki.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loomians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoomipediaNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    MeeleeAttack = table.Column<int>(type: "int", nullable: false),
                    MeeleeDefense = table.Column<int>(type: "int", nullable: false),
                    RangedAttack = table.Column<int>(type: "int", nullable: false),
                    RangedDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Type1 = table.Column<int>(type: "int", nullable: false),
                    Type2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loomians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoomianAbilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoomianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoomianAbilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoomianAbilities_Loomians_LoomianId",
                        column: x => x.LoomianId,
                        principalTable: "Loomians",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GameVersion = table.Column<int>(type: "int", nullable: false),
                    HealthTP = table.Column<int>(type: "int", nullable: false),
                    EnergyTP = table.Column<int>(type: "int", nullable: false),
                    MeeleeAttackTP = table.Column<int>(type: "int", nullable: false),
                    MeeleeDefenseTP = table.Column<int>(type: "int", nullable: false),
                    RangedAttackTP = table.Column<int>(type: "int", nullable: false),
                    RangedDefenseTP = table.Column<int>(type: "int", nullable: false),
                    SpeedTP = table.Column<int>(type: "int", nullable: false),
                    HealthUP = table.Column<int>(type: "int", nullable: false),
                    EnergyUP = table.Column<int>(type: "int", nullable: false),
                    MeeleeAttackUP = table.Column<int>(type: "int", nullable: false),
                    MeeleeDefenseUP = table.Column<int>(type: "int", nullable: false),
                    RangedAttackUP = table.Column<int>(type: "int", nullable: false),
                    RangedDefenseUP = table.Column<int>(type: "int", nullable: false),
                    SpeedUP = table.Column<int>(type: "int", nullable: false),
                    Personality1 = table.Column<int>(type: "int", nullable: false),
                    Personality2 = table.Column<int>(type: "int", nullable: false),
                    Personality3 = table.Column<int>(type: "int", nullable: false),
                    AbilityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoomianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sets_LoomianAbilities_AbilityId",
                        column: x => x.AbilityId,
                        principalTable: "LoomianAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sets_Loomians_LoomianId",
                        column: x => x.LoomianId,
                        principalTable: "Loomians",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Moves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SetId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moves_Sets_SetId",
                        column: x => x.SetId,
                        principalTable: "Sets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoomianAbilities_LoomianId",
                table: "LoomianAbilities",
                column: "LoomianId");

            migrationBuilder.CreateIndex(
                name: "IX_Moves_SetId",
                table: "Moves",
                column: "SetId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_AbilityId",
                table: "Sets",
                column: "AbilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Sets_LoomianId",
                table: "Sets",
                column: "LoomianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Moves");

            migrationBuilder.DropTable(
                name: "Sets");

            migrationBuilder.DropTable(
                name: "LoomianAbilities");

            migrationBuilder.DropTable(
                name: "Loomians");
        }
    }
}
