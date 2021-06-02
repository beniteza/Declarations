using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class Addedadditionalpropertiestoalltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetitionSignature_Petition_PublicDeclarationId",
                table: "PetitionSignature");

            migrationBuilder.RenameColumn(
                name: "PublicDeclarationId",
                table: "PetitionSignature",
                newName: "PetitionId");

            migrationBuilder.RenameColumn(
                name: "PersonName",
                table: "PetitionSignature",
                newName: "LastName");

            migrationBuilder.RenameIndex(
                name: "IX_PetitionSignature_PublicDeclarationId",
                table: "PetitionSignature",
                newName: "IX_PetitionSignature_PetitionId");

            migrationBuilder.RenameColumn(
                name: "DeclarationDate",
                table: "Petition",
                newName: "PetitionDate");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PetitionSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "PetitionSignature",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressLine",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_PetitionSignature_Petition_PetitionId",
                table: "PetitionSignature",
                column: "PetitionId",
                principalTable: "Petition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PetitionSignature_Petition_PetitionId",
                table: "PetitionSignature");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PetitionSignature");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "PetitionSignature");

            migrationBuilder.DropColumn(
                name: "AddressLine",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PetitionId",
                table: "PetitionSignature",
                newName: "PublicDeclarationId");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "PetitionSignature",
                newName: "PersonName");

            migrationBuilder.RenameIndex(
                name: "IX_PetitionSignature_PetitionId",
                table: "PetitionSignature",
                newName: "IX_PetitionSignature_PublicDeclarationId");

            migrationBuilder.RenameColumn(
                name: "PetitionDate",
                table: "Petition",
                newName: "DeclarationDate");

            migrationBuilder.AddForeignKey(
                name: "FK_PetitionSignature_Petition_PublicDeclarationId",
                table: "PetitionSignature",
                column: "PublicDeclarationId",
                principalTable: "Petition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
