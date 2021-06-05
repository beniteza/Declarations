using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class AddedUserIdasforeignkeyinPetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Petition",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "Petition",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Petition_UserId",
                table: "Petition",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Petition_AspNetUsers_UserId",
                table: "Petition",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Petition_AspNetUsers_UserId",
                table: "Petition");

            migrationBuilder.DropIndex(
                name: "IX_Petition_UserId",
                table: "Petition");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "Petition");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Petition",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
