using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Charwiki.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLoomianSetApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalTimestamp",
                table: "LoomianSets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApproverId",
                table: "LoomianSets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_LoomianSets_ApproverId",
                table: "LoomianSets",
                column: "ApproverId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets",
                column: "ApproverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoomianSets_Users_ApproverId",
                table: "LoomianSets");

            migrationBuilder.DropIndex(
                name: "IX_LoomianSets_ApproverId",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "ApprovalTimestamp",
                table: "LoomianSets");

            migrationBuilder.DropColumn(
                name: "ApproverId",
                table: "LoomianSets");
        }
    }
}
