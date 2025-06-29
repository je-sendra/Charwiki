using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class MakeApproverIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApproverId",
                table: "LoomianSets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApproverId",
                table: "LoomianSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
